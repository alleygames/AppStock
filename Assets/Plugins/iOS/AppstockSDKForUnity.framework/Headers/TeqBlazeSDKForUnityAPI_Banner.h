//
//  TeqBlazeSDKForUnityAPI_Banner.h
//  TeqBlazeSDKForUnity
//
//  Created by Maksym Kucherov on 2024-11-14.
//

#ifndef TeqBlazeSDKForUnityAPI_Banner_h
#define TeqBlazeSDKForUnityAPI_Banner_h

#include "TeqBlazeSDKForUnityTypes_Basic.h"

typedef const void *APSFUBannerAdUnitListenerRef;

APSFUBannerAdUnitListenerRef APSFUNewBannerAdUnit(APSFUAdIDForCallbacks idForCallbacks, APSFUAdSize adSize);

void APSFUReleaseBannerAdUnit(APSFUBannerAdUnitListenerRef const listenerRef);

void APSFUSetBannerAdUnitPlacementID(APSFUBannerAdUnitListenerRef const listenerRef,
                                    const char * const placementID);

void APSFUSetBannerAdUnitEndpointID(APSFUBannerAdUnitListenerRef const listenerRef,
                                   const char * const endpointID);

void APSFUSetBannerAdUnitAdSizes(APSFUBannerAdUnitListenerRef const listenerRef,
                                const APSFUAdSize *adSizes,
                                APSFUAdSizesCount count);

void APSFUClearBannerAdUnitAdSizes(APSFUBannerAdUnitListenerRef const listenerRef);

void APSFUSetBannerAdUnitRefreshInterval(APSFUBannerAdUnitListenerRef const listenerRef,
                                        double const autoRefreshDelay);

double APSFUGetBannerAdUnitRefreshInterval(APSFUBannerAdUnitListenerRef const listenerRef);

void APSFUSetBannerAdUnitAdFormat(APSFUBannerAdUnitListenerRef const listenerRef,
                                 APSFUAdFormat const adFormat);

void APSFUSetBannerAdUnitAdPosition(APSFUBannerAdUnitListenerRef const listenerRef,
                                   APSFUAdPosition const adPosition);

APSFUAdPosition APSFUGetBannerAdUnitAdPosition(APSFUBannerAdUnitListenerRef const listenerRef);

void APSFUSetBannerAdUnitAnchoredAdPosition(APSFUBannerAdUnitListenerRef const listenerRef,
                                           APSFUAnchoredAdPositionCode const anchoredAdPosition);

APSFUAdPosition APSFUGetBannerAdUnitAnchoredAdPosition(APSFUBannerAdUnitListenerRef const listenerRef);

void APSFUSetBannerAdUnitHidden(APSFUBannerAdUnitListenerRef const listenerRef,
                               APSFUBoolean const hidden);

APSFUBoolean APSFUGetBannerAdUnitHidden(APSFUBannerAdUnitListenerRef const listenerRef);

void APSFULoadBannerAdUnit(APSFUBannerAdUnitListenerRef const listenerRef);

void APSFUStopBannerAdUnitAutoRefresh(APSFUBannerAdUnitListenerRef const listenerRef);

#endif /* TeqBlazeSDKForUnityAPI_Banner_h */
