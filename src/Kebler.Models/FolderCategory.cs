﻿using Caliburn.Micro;
using System.Diagnostics;
using System.IO;

namespace Kebler.Models
{
    [DebuggerDisplay("{FolderName} ({Count})")]
    public class FolderCategory : PropertyChangedBase
    {
        public readonly DirectoryInfo Dir;

        public FolderCategory(string fullPath)
        {
            FullPath = NormalizePath(fullPath);
            Dir = new DirectoryInfo(FullPath);
        }

        public string FolderName => Dir.Name;
        public string FullPath { get; }

        private string _title = "~";
        public string Title
        {
            get => $"{_title} ({Count})";
            set
            {
                Set(ref _title, value);
            }
        }

        private int _count;
        public int Count
        {
            get => _count;
            set
            {
                Set(ref _count, value);
                NotifyOfPropertyChange(nameof(Title));
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is FolderCategory cat) return cat.FullPath.Equals(FullPath);
            return false;
        }





        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = FullPath.GetHashCode();
                hashCode = (hashCode * 397) ^ hashCode;
                return hashCode;
            }
        }

        public static string NormalizePath(string path)
        {
            if (!Path.EndsInDirectorySeparator(path)) path = $"{path}{Path.AltDirectorySeparatorChar}";

            return path;
        }

        public override string ToString()
        {
            return $"'{FullPath}'";
        }
    }

    [DebuggerDisplay("{Title} ({Count})")]
    public class StatusCategory : PropertyChangedBase
    {
        private string _count = "~";
        public string Title { get; set; } = "~";
        public Enums.Categories Cat { get; set; }

        public string Count
        {
            get => _count;
            set
            {
                var dd = Set(ref _count, value);
            }
        }
    }
}