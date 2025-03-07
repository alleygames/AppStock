//
//  TeqBlazeSDKForUnityAPI_Native.h
//  TeqBlazeSDKForUnity
//
//  Created by Maksym Kucherov on 2024-11-14.
//

#ifndef TeqBlazeSDKForUnityAPI_Native_h
#define TeqBlazeSDKForUnityAPI_Native_h

#include "TeqBlazeSDKForUnityTypes_Native.h"
#include "TeqBlazeSDKForUnityTypes_String.h"



// MARK: - AppstockNativeAdUnit
typedef const void *APSFUNativeAdUnitRef;

APSFUNativeAdUnitRef APSFUNewNativeAdUnit(APSFUNativeAdUnitData const adUnitData);

void APSFUReleaseNativeAdUnit(APSFUNativeAdUnitRef const adUnitRef);

void APSFULoadNativeAd(APSFUNativeAdUnitRef const adUnitRef, APSFUAdIDForCallbacks const idForCallbacks);



// MARK: - AppstockNativeAd +listener
typedef const void *APSFUNativeAdListenerRef;
typedef int64_t APSFUNativeContentCount;

APSFUNativeAdListenerRef APSFUNewNativeAdListener(APSFUAdIDForCallbacks const idForCallbacks,
                                                APSFUNativeAdRef const nativeAdRef);

void APSFUReleaseNativeAd(APSFUNativeAdListenerRef const listenerRef);

APSFUNativeContentCount APSFUGetNativeAdTitlesCount(APSFUNativeAdListenerRef const listenerRef);
APSFUNativeContentCount APSFUGetNativeAdImagesCount(APSFUNativeAdListenerRef const listenerRef);
APSFUNativeContentCount APSFUGetNativeAdDataObjectsCount(APSFUNativeAdListenerRef const listenerRef);

APSFUOutStringRef APSFUGetNativeAdTitle(APSFUNativeAdListenerRef const listenerRef);
APSFUOutStringRef APSFUGetNativeAdImageUrl(APSFUNativeAdListenerRef const listenerRef);
APSFUOutStringRef APSFUGetNativeAdIconUrl(APSFUNativeAdListenerRef const listenerRef);
APSFUOutStringRef APSFUGetNativeAdSponsoredBy(APSFUNativeAdListenerRef const listenerRef);
APSFUOutStringRef APSFUGetNativeAdCallToAction(APSFUNativeAdListenerRef const listenerRef);

APSFUBoolean APSFURegisterNativeAd(APSFUNativeAdListenerRef const listenerRef);



// MARK: - AppstockNativeTitle
typedef const void *APSFUNativeTitleRef;

APSFUNativeTitleRef APSFUGetNativeAdTitleAt(APSFUNativeAdListenerRef const listenerRef,
                                          APSFUNativeContentCount const titleIndex);

void APSFUReleaseNativeTitle(APSFUNativeTitleRef const titleRef);

APSFUOutStringRef APSFUGetNativeTitleText(APSFUNativeTitleRef const titleRef);



// MARK: - AppstockNativeData
typedef const void *APSFUNativeDataRef;

APSFUNativeDataRef APSFUGetNativeAdDataAt(APSFUNativeAdListenerRef const listenerRef,
                                        APSFUNativeContentCount const dataIndex);

void APSFUReleaseNativeData(APSFUNativeDataRef const dataRef);

APSFUNullableInt APSFUGetNativeDataTypeCode(APSFUNativeDataRef const dataRef);

APSFUOutStringRef APSFUGetNativeDataValue(APSFUNativeDataRef const dataRef);



// MARK: - AppstockNativeImage
typedef const void *APSFUNativeImageRef;

APSFUNativeImageRef APSFUGetNativeAdImageAt(APSFUNativeAdListenerRef const listenerRef,
                                          APSFUNativeContentCount const imageIndex);

void APSFUReleaseNativeImage(APSFUNativeImageRef const imageRef);

APSFUNullableInt APSFUGetNativeImageTypeCode(APSFUNativeImageRef const imageRef);

APSFUOutStringRef APSFUGetNativeImageURL(APSFUNativeImageRef const imageRef);


#endif /* TeqBlazeSDKForUnityAPI_Native_h */
