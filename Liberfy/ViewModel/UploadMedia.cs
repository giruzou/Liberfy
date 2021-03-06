﻿using CoreTweet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static Liberfy.Defines;
using System.Drawing;

namespace Liberfy.ViewModel
{
	internal class UploadMedia : NotificationObject, IProgress<UploadChunkedProgressInfo>, IDisposable
	{
		private UploadMedia(BitmapSource bmpSource, MediaType mediaType, string ext)
		{
			var pngEnc = new PngBitmapEncoder();
			var memStr = new MemoryStream();

			// 画像データをストリームに保存
			PreviewImage = new BitmapImage();
			pngEnc.Frames.Add(BitmapFrame.Create(bmpSource));
			pngEnc.Save(memStr);
			pngEnc = null;

			// ストリームからプレビュー画像の生成
			memStr.Position = 0;
			SourceStream = memStr;
			PreviewImage.BeginInit();
			PreviewImage.StreamSource = memStr;
			PreviewImage.EndInit();

			MediaType = mediaType;
			ViewExtension = ext;
		}

		private UploadMedia(string filePath)
		{
			var ext = Path.GetExtension(filePath).ToLower();

			if (ext.Equals(".gif") && IsAnimatedGif(ext))
			{
				MediaType = MediaType.AnimatedGifFile;
				PreviewImage = new BitmapImage(new Uri(filePath));
			}
			else if (VideoExtensions.Contains(ext))
			{
				MediaType = MediaType.VideoFile;
				UseChunkedUpload = true;
			}
			else if (ImageExtensions.Contains(ext))
			{
				MediaType = MediaType.ImageFile;

				if (ext != ".webp")
				{
					PreviewImage = new BitmapImage(new Uri(filePath));
				}
			}
			else
			{
				throw new NotSupportedException();
			}

			FilePath = filePath;
			filePath = Path.GetFileName(filePath);
			ViewExtension = ext.Substring(1).ToUpper();
			SourceStream = File.OpenRead(FilePath);

			if (PreviewImage?.CanFreeze ?? false)
			{
				PreviewImage.Freeze();
			}
		}

		public static UploadMedia FromBitmapSource(BitmapSource bmpSource, string ext = "CLIP")
		{
			return new UploadMedia(bmpSource, MediaType.Image, ext);
		}

		public static UploadMedia FromArtwork(ArtworkItem artwork)
		{
			return new UploadMedia(artwork.Image, MediaType.Image, "ARTW");
		}

		public static UploadMedia FromFile(string filepath)
		{
			return new UploadMedia(filepath);
		}

		private static bool IsAnimatedGif(string filename) => ImageAnimator.CanAnimate(Image.FromFile(filename));

		public string FileName { get; private set; }

		public string FilePath { get; private set; }

		public MediaType MediaType { get; }

		public string ViewExtension { get; }

		public BitmapImage PreviewImage { get; private set; }

		public long? UploadId { get; private set; }

		private string _description;
		public string Description
		{
			get => _description;
			private set => SetProperty(ref _description, value);
		}

		private double _uploadProgress;
		public double UploadProgress
		{
			get => _uploadProgress;
			private set => SetProperty(ref _uploadProgress, value);
		}

		private bool _isUploadFailed;
		public bool IsUploadFailed
		{
			get => _isUploadFailed;
			private set => SetProperty(ref _isUploadFailed, value);
		}

		private bool _isUploading;
		public bool IsUploading
		{
			get => _isUploading;
			private set => SetProperty(ref _isUploading, value);
		}

		private bool _isTweetPosting;
		public bool IsTweetPosting => _isTweetPosting;

		public bool UseChunkedUpload { get; }

		public Stream SourceStream { get; private set; }

		private void CleanUploadState()
		{
			IsUploading = false;
			UploadProgress = 0.0d;
			IsUploadFailed = false;
		}

		public void SetIsTweetPosting(bool value)
		{
			SetProperty(ref _isTweetPosting, value, nameof(IsTweetPosting));
		}

		public async Task Upload(Tokens tokens)
		{
			bool isVideoUpload = (MediaType & MediaType.Video) != 0;

			var uploadType = isVideoUpload
				? UploadMediaType.Video
				: UploadMediaType.Image;

			CleanUploadState();

			IsUploading = true;

			SourceStream.Position = 0;

			try
			{
				Task<MediaUploadResult> task;

				if (isVideoUpload)
				{
					task = tokens.Media.UploadChunkedAsync(
						media: SourceStream,
						mediaType: uploadType,
						progress: this);
				}
				else
				{
					task = Task.Run(() => tokens.Media.Upload(SourceStream));
				}

				var result = await task.ConfigureAwait(true);

				UploadId = result.MediaId;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex);
				IsUploadFailed = true;
			}
			finally
			{
				IsUploading = false;
			}
		}

		public bool IsAvailableUploadId() => UploadId.HasValue && UploadId > 0;

		public void Report(UploadChunkedProgressInfo info)
		{
			UploadProgress = (double)info.BytesSent / info.TotalBytesToSend;
		}

		public void Dispose()
		{
			if (SourceStream != null)
			{
				SourceStream.Dispose();
				SourceStream = null;
			}

			if (PreviewImage != null)
			{
				PreviewImage = null;
			}

			FilePath = null;
			FileName = null;
		}
	}

	[Flags]
	internal enum MediaType : uint
	{
		None = 0,
		File = 0x01,
		Image = 0x02,
		Video = 0x04,
		AnimatedGif = 0x08,
		ImageFile = Image | File,
		VideoFile = Video | File,
		AnimatedGifFile = AnimatedGif | File,
	}
}
