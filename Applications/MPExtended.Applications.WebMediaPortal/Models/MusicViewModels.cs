﻿#region Copyright (C) 2012 MPExtended
// Copyright (C) 2012 MPExtended Developers, http://mpextended.github.com/
// 
// MPExtended is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// MPExtended is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with MPExtended. If not, see <http://www.gnu.org/licenses/>.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPExtended.Services.MediaAccessService.Interfaces.Music;

namespace MPExtended.Applications.WebMediaPortal.Models
{
    public class ArtistViewModel
    {
        public WebMusicArtistDetailed Artist { get; set; }
        public IEnumerable<WebMusicAlbumBasic> Albums { get; set; }
    }

    public class AlbumViewModel
    {
        public AlbumViewModel() { }
        public AlbumViewModel(WebMusicAlbumBasic album)
        {
            Album = album;
        }

        public WebMusicAlbumBasic Album { get; set; }
        public IEnumerable<WebMusicTrackDetailed> Tracks { get; set; }
    }

    public class MusicTrackViewModel
    {
        public MusicTrackViewModel(){}
        public MusicTrackViewModel(WebMusicTrackDetailed track)
        {
            Track = track;
        }
        public WebMusicTrackDetailed Track { get; set; }
    }
}