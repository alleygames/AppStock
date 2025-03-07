//
//  TeqBlazeSDKForUnityAPI_Interstitial.h
//  TeqBlazeSDKForUnity
//
//  Created by Maksym Kucherov on 2024-11-14.
//

#ifndef TeqBlazeSDKForUnityAPI_Interstitial_h
#define TeqBlazeSDKForUnityAPI_Interstitial_h

#include "TeqBlazeSDKForUnityTypes_Basic.h"

typedef const void *APSFUInterstitialAdUnitListenerRef;

APSFUInterstitialAdUnitListenerRef APSFUNewInterstitialAdUnit(APSFUAdIDForCallbacks idForCallbacks);

void APSFUReleaseInterstitialAdUnit(APSFUInterstitialAdUnitListenerRef const listenerRef);

void APSFUSetInterstitialAdUnitPlacementID(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                          const char * const placementID);

void APSFUSetInterstitialAdUnitEndpointID(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                         const char * const endpointID);

void APSFUSetInterstitialAdUnitAdFormats(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                        const APSFUAdFormat * const adFormats,
                                        APSFUAdFormatsCount const count);

void APSFUSetInterstitialAdUnitAdSizes(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                      const APSFUAdSize *adSizes,
                                      APSFUAdSizesCount count);

void APSFUClearInterstitialAdUnitAdSizes(APSFUInterstitialAdUnitListenerRef const listenerRef);

void APSFUSetInterstitialAdUnitSkipButtonArea(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                             double const skipButtonArea);

void APSFUSetInterstitialAdUnitSkipButtonPosition(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                                 APSFUAdElementPosition const skipButtonPosition);

void APSFUSetInterstitialAdUnitSkipDelay(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                        double const skipDelay);

void APSFUSetInterstitialAdUnitMuted(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                    APSFUBoolean const muted);

void APSFUSetInterstitialAdUnitSoundButtonVisible(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                                 APSFUBoolean const soundButtonVisible);

void APSFUSetInterstitialAdUnitCloseButtonArea(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                              double const closeButtonArea);

void APSFUSetInterstitialAdUnitCloseButtonPosition(APSFUInterstitialAdUnitListenerRef const listenerRef,
                                                  APSFUAdElementPosition const closeButtonPosition);

APSFUBoolean APSFUGetInterstitialAdUnitIsLoaded(APSFUInterstitialAdUnitListenerRef const listenerRef);

void APSFULoadInterstitialAdUnit(APSFUInterstitialAdUnitListenerRef const listenerRef);

void APSFUShowInterstitialAdUnit(APSFUInterstitialAdUnitListenerRef const listenerRef);

#endif /* TeqBlazeSDKForUnityAPI_Interstitial_h */
