﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Amplius
{
    /// <summary>
    /// A wrapper around a major, minor, patch and extra (snapshot, etc) data.
    /// </summary>
    public sealed class Version : IComparable<Version>, ICloneable
    {
        public int Major => major;
        public int Minor => minor;
        public int Patch => patch;
        public string Extra => extra;

        private int major;
        private int minor;
        private int patch;
        private string extra;

        private string label => extra != "" ? $"-{extra}" : "";

        public Version(int major, int minor, int patch, string extra)
        {
            this.major = major;
            this.minor = minor;
            this.patch = patch;
            this.extra = extra;
        }

        public override bool Equals(object other) => 
            other is Version
            && major == ((Version)other).major
            && minor == ((Version)other).minor
            && patch == ((Version)other).patch
            && extra == ((Version)other).extra;
        public override int GetHashCode() => major * (minor + (patch * extra.GetHashCode())).GetHashCode();
        public override string ToString() => $"{major}.{minor}.{patch}{label}";

        public int CompareTo([AllowNull] Version other)
        {
            if (major > other.major) return 1;
            else if (major < other.major) return -1;
            else if (major == other.major && minor > other.minor) return 1;
            else if (major == other.major && minor < other.minor) return -1;
            else if (major == other.major && minor == other.minor && patch > other.patch) return 1;
            else if (major == other.major && minor == other.minor && patch < other.patch) return -1;
            else return 0;
        }
        public object Clone() => new Version(major, minor, patch, extra);

        /// <summary>
        /// Creates a <see cref="Version"/> object from the <paramref name="versionString"/>.
        /// </summary>
        /// <param name="versionString"></param>
        /// <returns></returns>
        public static Version FromString(string versionString)
        {
            string[] data = versionString.Split('.', '-');

            Version version;

            try
            {
                version = new Version(
                    Convert.ToInt32(data[0]),
                    Convert.ToInt32(data[1]),
                    Convert.ToInt32(data[2]),
                    data.Length > 3 ? data[3] : "");
            }
            catch (Exception) { throw new InvalidVersionString(versionString); }

            return version;
        }

        public static implicit operator Version(string versionString) => FromString(versionString);
        public static explicit operator string(Version version) => version.ToString();
    }

    /// <summary>
    /// A simplistic exception for invalid version strings.
    /// </summary>
    public sealed class InvalidVersionString : Exception
    {
        public InvalidVersionString(string attempted) : base($"Invalid string: Cannot convert {attempted} to a version.") { }
    }
}