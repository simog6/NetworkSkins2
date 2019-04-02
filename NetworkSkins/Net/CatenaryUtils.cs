﻿using System;

// TODO add support for railway

namespace NetworkSkins.Net
{
    public static class CatenaryUtils
    {
        private static readonly string[] DoubleCatenaryNames = new[]
        {
            "RailwayPowerline",
            "774449380.Catenary Type NL2A_Data",
            "774449380.Catenary Type NL2B_Data",
            "774449380.Catenary Type DE2A_Data",
            "774449380.Catenary Type PRR2A_Data",
            "774449380.Catenary Type PRR2B_Data",
            "774449380.Catenary Type JP2A_Data",
            "774449380.Catenary Type EXPO2A_Data",
        };

        private static readonly string[] SingleCatenaryNames = new[]
        {
            "RailwayPowerline Singular",
            "774449380.Catenary Type NL1A_Data",
            "774449380.Catenary Type NL1B_Data",
            "774449380.Catenary Type DE1A_Data",
            "774449380.Catenary Type PRR 1A_Data",
            "774449380.Catenary Type PRR 1A_Data",
            "774449380.Catenary Type JP1A_Data",
            "774449380.Catenary Type EXPO1A_Data",
        };

        public static bool IsCatenaryProp(PropInfo prop)
        {
            return IsDoubleRailCatenaryProp(prop) || IsSingleRailCatenaryProp(prop);
        }

        public static bool IsDoubleRailCatenaryProp(PropInfo prop)
        {
            if (prop == null) return false;
            return Array.IndexOf(DoubleCatenaryNames, prop.name) != -1;
        }

        public static bool IsSingleRailCatenaryProp(PropInfo prop)
        {
            if (prop == null) return false;
            return Array.IndexOf(SingleCatenaryNames, prop.name) != -1;
        }

        public static void CorrectCatenaryPropAngle(NetLaneProps.Prop laneProp)
        {
            var propName = laneProp.m_finalProp?.name;

            // correct rotation for Tim's catenaries!
            if (propName != null && propName.StartsWith("774449380"))
            {
                laneProp.m_angle = 180f;
            }
        }

        public static bool IsWireSegment(NetInfo.Segment segment)
        {
            return segment?.m_material?.shader?.name == "Custom/Net/Electricity";
        }
    }
}