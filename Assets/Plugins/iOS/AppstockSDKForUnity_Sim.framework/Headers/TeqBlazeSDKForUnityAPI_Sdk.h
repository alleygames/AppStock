//
//  TeqBlazeSDKForUnityAPI_Sdk.h
//  TeqBlazeSDKForUnity
//
//  Created by Maksym Kucherov on 2024-11-28.
//

#ifndef TeqBlazeSDKForUnityAPI_Sdk_h
#define TeqBlazeSDKForUnityAPI_Sdk_h

#include "TeqBlazeSDKForUnityTypes_Sdk.h"
#include "TeqBlazeSDKForUnityTypes_String.h"


// MARK: - TeqBlaze

void APSFUSetSdkDebugRequests(APSFUBoolean const debugRequests);
APSFUBoolean APSFUGetSdkDebugRequests(void);

void APSFUSetSdkEndpointID(const char * const endpointID);

void APSFUSetSdkShouldAssignNativeAssetId(APSFUBoolean const shouldAssignNativeAssetID);
APSFUBoolean APSFUGetSdkShouldAssignNativeAssetId(void);

void APSFUSetSdkLogLevel(APSFULogLevelCode const logLevel);
APSFULogLevelCode APSFUGetSdkLogLevel(void);

void APSFUSetSdkExternalUserIDs(const APSFUExternalUserID * const externalUserIDs,
                            APSFUExternalUserIDsCount const count);
APSFUExternalUserIDsCount APSFUGetSdkExternalUserIDsCount(void);
APSFUExternalUserIDRef APSFUGetSdkExternalUserID(APSFUExternalUserIDsCount const idIndex);

APSFUOutStringRef APSFUGetSdkVersion(void);
APSFUOutStringRef APSFUGetOmSdkVersion(void);

void APSFUSetSdkTimeoutMillis(int64_t const timeoutMillis);
int64_t APSFUGetSdkTimeoutMillis(void);

void APSFUSetSdkAdRequestTimeout(double const adRequestTimeout);
double APSFUGetSdkAdRequestTimeout(void);

void APSFUSetSdkAdRequestTimeoutPreRenderContent(double const adRequestTimeoutPreRenderContent);
double APSFUGetSdkAdRequestTimeoutPreRenderContent(void);


// MARK: - AppstockTargeting

void APSFUSetTargetingCustomUserData(const char * userCustomData);
APSFUOutStringRef APSFUGetTargetingCustomUserData(void);

void APSFUSetTargetingExternalUserIDs(const APSFUExternalUserID * const externalUserIDs,
                                     APSFUExternalUserIDsCount const count);
APSFUExternalUserIDsCount APSFUGetTargetingExternalUserIDsCount(void);
APSFUExternalUserIDRef APSFUGetTargetingExternalUserID(APSFUExternalUserIDsCount const idIndex);

void APSFUSetTargetingUserExt(const char * const userExtJson);
APSFUOutStringRef APSFUGetTargetingUserExt(void);

void APSFUSetTargetingPublisherName(const char * const publisherName);
APSFUOutStringRef APSFUGetTargetingPublisherName(void);

void APSFUSetTargetingSourceApp(const char * const sourceapp);
APSFUOutStringRef APSFUGetTargetingSourceApp(void);

void APSFUSetTargetingStoreURL(const char * const storeURL);
APSFUOutStringRef APSFUGetTargetingStoreURL(void);

void APSFUSetTargetingDomain(const char * const domain);
APSFUOutStringRef APSFUGetTargetingDomain(void);

void APSFUSetTargetingITunesID(const char * const itunesID);
APSFUOutStringRef APSFUGetTargetingITunesID(void);

void APSFUSetTargetingCoordinate(APSFUOptionalCoordinate2D const coordinate);
APSFUOptionalCoordinate2D APSFUGetTargetingCoordinate(void);

void APSFUSetTargetingSubjectToCOPPA(APSFUOptionalBool const subjectToCOPPA);
APSFUOptionalBool APSFUGetTargetingSubjectToCOPPA(void);

void APSFUAddTargetingAppKeyword(const char * const keyword);
long APSFUGetTargetingAppKeywordsCount(void);
APSFUOutStringRef APSFUGetTargetingAppKeyword(long const keywordIndex);


// MARK: - ExternalUserID

APSFUOutStringRef APSFUGetExternalUserIdSource(APSFUExternalUserIDRef const externalIdRef);
APSFUOutStringRef APSFUGetExternalUserIdIdentifier(APSFUExternalUserIDRef const externalIdRef);
APSFUNullableInt APSFUGetExternalUserIdAType(APSFUExternalUserIDRef const externalIdRef);
APSFUOutStringRef APSFUGetExternalUserIdExt(APSFUExternalUserIDRef const externalIdRef);

#endif /* TeqBlazeSDKForUnityAPI_Sdk_h */
