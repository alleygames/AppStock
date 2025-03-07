//
//  TeqBlazeSDKForUnityAPI_Rewarded.h
//  TeqBlazeSDKForUnity
//
//  Created by Maksym Kucherov on 2024-11-14.
//

#ifndef TeqBlazeSDKForUnityAPI_Rewarded_h
#define TeqBlazeSDKForUnityAPI_Rewarded_h

#include "TeqBlazeSDKForUnityTypes_Basic.h"

typedef const void *APSFURewardedAdUnitListenerRef;

APSFURewardedAdUnitListenerRef APSFUNewRewardedAdUnit(APSFUAdIDForCallbacks idForCallbacks);

void APSFUReleaseRewardedAdUnit(APSFURewardedAdUnitListenerRef const listenerRef);

void APSFUSetRewardedAdUnitPlacementID(APSFURewardedAdUnitListenerRef const listenerRef,
                                      const char * const placementID);

void APSFUSetRewardedAdUnitEndpointID(APSFURewardedAdUnitListenerRef const listenerRef,
                                     const char * const endpointID);

void APSFUSetRewardedAdUnitAdFormats(APSFURewardedAdUnitListenerRef const listenerRef,
                                    const APSFUAdFormat * const adFormats,
                                    APSFUAdFormatsCount const count);

void APSFUSetRewardedAdUnitAdSizes(APSFURewardedAdUnitListenerRef const listenerRef,
                                  const APSFUAdSize *adSizes,
                                  APSFUAdSizesCount count);

void APSFUClearRewardedAdUnitAdSizes(APSFURewardedAdUnitListenerRef const listenerRef);

void APSFUSetRewardedAdUnitMuted(APSFURewardedAdUnitListenerRef const listenerRef,
                                APSFUBoolean const muted);

void APSFUSetRewardedAdUnitSoundButtonVisible(APSFURewardedAdUnitListenerRef const listenerRef,
                                             APSFUBoolean const soundButtonVisible);

void APSFUSetRewardedAdUnitCloseButtonArea(APSFURewardedAdUnitListenerRef const listenerRef,
                                          double const closeButtonArea);

void APSFUSetRewardedAdUnitCloseButtonPosition(APSFURewardedAdUnitListenerRef const listenerRef,
                                              APSFUAdElementPosition const closeButtonPosition);

APSFUBoolean APSFUGetRewardedAdUnitIsLoaded(APSFURewardedAdUnitListenerRef const listenerRef);

void APSFULoadRewardedAdUnit(APSFURewardedAdUnitListenerRef const listenerRef);

void APSFUShowRewardedAdUnit(APSFURewardedAdUnitListenerRef const listenerRef);

#endif /* TeqBlazeSDKForUnityAPI_Rewarded_h */
