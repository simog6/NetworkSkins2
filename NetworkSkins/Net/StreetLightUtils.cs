﻿using JetBrains.Annotations;

namespace NetworkSkins.Net
{
    public static class StreetLightUtils
    {
        public static bool HasStreetLights(NetInfo prefab)
        {
            if (prefab.m_lanes == null) return false;

            foreach (var lane in prefab.m_lanes)
            {
                var laneProps = lane?.m_laneProps?.m_props;
                if (laneProps == null) continue;

                foreach (var laneProp in laneProps)
                {
                    if (IsStreetLightProp(laneProp?.m_finalProp))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        [CanBeNull]
        public static PropInfo GetDefaultStreetLight(NetInfo prefab)
        {
            return NetUtil.GetMatchingLaneProp(prefab, laneProp => IsStreetLightProp(laneProp.m_finalProp))?.m_finalProp;
        }

        public static float GetDefaultRepeatDistance(NetInfo prefab)
        {
            return NetUtil.GetMatchingLaneProp(prefab, laneProp => IsStreetLightProp(laneProp.m_finalProp))?.m_repeatDistance ?? 40f;
        }

        public static bool IsStreetLightProp(PropInfo prefab)
        {
            if (prefab == null) return false;

            if (prefab.m_class.m_service == ItemClass.Service.Road ||
                prefab.m_class.m_subService == ItemClass.SubService.PublicTransportPlane ||
                prefab.name.ToLower().Contains("streetlamp") || prefab.name.ToLower().Contains("streetlight") || prefab.name.ToLower().Contains("lantern"))
            {
                if (prefab.m_effects != null && prefab.m_effects.Length > 0)
                {
                    if (prefab.name.ToLower().Contains("taxiway")) return false;
                    if (prefab.name.ToLower().Contains("runway")) return false;

                    foreach (var effect in prefab.m_effects)
                    {
                        if (effect.m_effect is LightEffect)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
