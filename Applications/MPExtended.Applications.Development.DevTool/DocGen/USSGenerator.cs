﻿#region Copyright (C) 2011-2012 MPExtended
// Copyright (C) 2011-2012 MPExtended Developers, http://mpextended.github.com/
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
using System.Text;
using System.Reflection;

namespace MPExtended.Applications.Development.DevTool.DocGen
{
    internal class USSGenerator : Generator
    {
        public USSGenerator(Assembly assembly)
        {
            this.Assembly = assembly;
            this.JsonAPI = this.Assembly.GetType("MPExtended.Services.UserSessionService.Interfaces.IUserSessionService");
            this.Enumerations = new List<Type>() {
                this.Assembly.GetType("MPExtended.Services.UserSessionService.Interfaces.WebPowerMode"),
            };
        }
    }
}
