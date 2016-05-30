﻿using System;
using System.IO;
using System.Text.RegularExpressions;

namespace medis.Api.Extensions
{
    public static class StringExtensions
    {
        public static string SanitizeString(this string value){
            return Regex.Replace(value, @"[^a-zA-Z0-9\s-]", string.Empty);
        }

        public static string Slugify(this string value) {
            var sanitizedString = SanitizeString(value);

            return sanitizedString.Trim()
                .ToLower()
                .Replace(" ", "-");
        }

        public static string ToGfsFilename(this string value) {
            var filename = Path.GetFileNameWithoutExtension(value.SanitizeWebApiContentDispositionFilename())
                .Slugify();

            return filename.AddShortGuid();
        }

        public static string AddShortGuid(this string value) {
            var guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            return $"{value}__{guid}";
        }

        public static string SanitizeWebApiContentDispositionFilename(this string value) {
            return value.Trim('"');
        }
    }
}