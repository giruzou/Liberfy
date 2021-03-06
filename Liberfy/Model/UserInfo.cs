﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using System.Windows.Media;

namespace Liberfy
{
	internal class UserInfo : NotificationObject, IObjectInfo<User>, IEquatable<UserInfo>, IEquatable<User>
	{
		public bool IsContributorsEnabled { get; private set; }

		public DateTimeOffset CreatedAt { get; }

		public bool IsDefaultProfile { get; private set; }

		public bool IsDefaultProfileImage { get; private set; }

		public string Description { get; private set; }

		public UserEntities Entities { get; private set; }

		public int FavouritesCount { get; private set; }

		public bool IsFollowRequestSent { get; private set; }

		public int FollowersCount { get; private set; }

		public int FriendsCount { get; private set; }

		public bool HasExtendedProfile { get; private set; }

		public bool IsGeoEnabled { get; private set; }

		public long Id { get; }

		public bool IsTranslator { get; private set; }

		public bool IsTranslationEnabled { get; private set; }

		public string Language { get; private set; }

		public int ListedCount { get; private set; }

		public string Location { get; private set; }

		public bool IsMuting { get; private set; }

		public string Name { get; private set; }

		public bool NeedsPhoneVerification { get; private set; }

		public Color ProfileBackgroundColor { get; private set; }

		public string ProfileBackgroundImageUrl { get; private set; }

		public string ProfileBackgroundImageUrlHttps { get; private set; }

		public bool IsProfileBackgroundTile { get; private set; }

		public string ProfileBannerUrl { get; private set; }

		public string ProfileImageUrl { get; private set; }

		public Color ProfileLinkColor { get; private set; }

		public Place ProfileLocation { get; private set; }

		public Color ProfileSidebarBorderColor { get; private set; }

		public Color ProfileSidebarFillColor { get; private set; }

		public Color ProfileTextColor { get; private set; }

		public bool IsProfileUseBackgroundImage { get; private set; }

		public bool IsProtected { get; private set; }

		public string ScreenName { get; private set; }

		public bool IsShowAllInlineMedia { get; private set; }

		public Status Status { get; private set; }

		public int StatusesCount { get; private set; }

		public bool IsSuspended { get; private set; }

		public string TimeZone { get; private set; }

		public string TranslatorType { get; private set; }

		public string Url { get; private set; }

		public int UtcOffset { get; private set; }

		public bool IsVerified { get; private set; }

		public string WithheldInCountries { get; private set; }

		public string WithheldScope { get; private set; }

		public DateTime UpdatedAt { get; private set; }

		public UserInfo(User user)
		{
			Id = user.Id ?? Id;
			CreatedAt = user.CreatedAt;

			Update(user);
		}

		public UserInfo(long id, string name, string screenName, bool isProtected, string profileImageUrl)
		{
			Id = id;
			Name = name;
			ScreenName = screenName;
			IsProtected = isProtected;
			ProfileImageUrl = profileImageUrl;

			UpdatedAt = DateTime.Now;
		}

		private static Color ToColor(string name)
		{
			return string.IsNullOrWhiteSpace(name)
				? Colors.Transparent
				: (Color)ColorConverter.ConvertFromString("#" + name);
		}

		public void Update(User item)
		{
			IsContributorsEnabled = item.IsContributorsEnabled;
			IsDefaultProfile = item.IsDefaultProfile;
			IsDefaultProfileImage = item.IsDefaultProfileImage;
			Description = item.Description;
			Entities = item.Entities;
			FavouritesCount = item.FavouritesCount;
			IsFollowRequestSent = item.IsFollowRequestSent ?? IsFollowRequestSent;
			FollowersCount = item.FollowersCount;
			FriendsCount = item.FriendsCount;
			//HasExtendedProfile = item.HasExtendedProfile ?? HasExtendedProfile;
			IsGeoEnabled = item.IsGeoEnabled;
			IsTranslator = item.IsTranslator;
			IsTranslationEnabled = item.IsTranslationEnabled;
			Language = item.Language;
			ListedCount = item.ListedCount ?? ListedCount;
			Location = item.Location;
			IsMuting = item.IsMuting ?? IsMuting;
			Name = item.Name;
			NeedsPhoneVerification = item.NeedsPhoneVerification ?? NeedsPhoneVerification;
			ProfileBackgroundColor = ToColor(item.ProfileBackgroundColor);
			ProfileBackgroundImageUrl = item.ProfileBackgroundImageUrl;
			ProfileBackgroundImageUrlHttps = item.ProfileBackgroundImageUrlHttps;
			IsProfileBackgroundTile = item.IsProfileBackgroundTile;
			ProfileBannerUrl = item.ProfileBannerUrl;
			ProfileImageUrl = item.ProfileImageUrl;
			ProfileLinkColor = ToColor(item.ProfileLinkColor);
			ProfileLocation = item.ProfileLocation;
			ProfileSidebarBorderColor = ToColor(item.ProfileSidebarBorderColor);
			ProfileSidebarFillColor = ToColor(item.ProfileSidebarFillColor);
			ProfileTextColor = ToColor(item.ProfileTextColor);
			IsProfileUseBackgroundImage = item.IsProfileUseBackgroundImage;
			IsProtected = item.IsProtected;
			ScreenName = item.ScreenName;
			IsShowAllInlineMedia = item.IsShowAllInlineMedia ?? IsShowAllInlineMedia;
			Status = item.Status;
			StatusesCount = item.StatusesCount;
			IsSuspended = item.IsSuspended ?? IsSuspended;
			TimeZone = item.TimeZone;
			//TranslatorType = item.TranslatorType;
			Url = item.Url;
			UtcOffset = item.UtcOffset ?? UtcOffset;
			IsVerified = item.IsVerified;
			WithheldInCountries = item.WithheldInCountries;
			WithheldScope = item.WithheldScope;

			UpdatedAt = DateTime.Now;

			RaisePropertyChanged("");
		}

		public bool Equals(UserInfo other) => Equals(Id, other?.Id);

		public bool Equals(User other) => Equals(Id, other?.Id);

		public override bool Equals(object obj)
		{
			return (obj is UserInfo userInfo && Equals(userInfo))
				|| (obj is User user && Equals(user));
		}

		public override int GetHashCode() => Id.GetHashCode();
	}
}
