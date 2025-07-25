/* tslint:disable */
/* eslint-disable */
/**
 * BikeService.Server | v1
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.0
 *
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import * as runtime from "../runtime";
import type { BikeResponse, PostBikeRequest, PutBikeRequest } from "../models/index";
import {
    BikeResponseFromJSON,
    BikeResponseToJSON,
    PostBikeRequestFromJSON,
    PostBikeRequestToJSON,
    PutBikeRequestFromJSON,
    PutBikeRequestToJSON
} from "../models/index";

export interface ApiBikeDeleteRequest {
    id?: string;
}

export interface ApiBikeIdGetRequest {
    id: string;
}

export interface ApiBikePostRequest {
    postBikeRequest: PostBikeRequest;
}

export interface ApiBikePutRequest {
    putBikeRequest: PutBikeRequest;
}

/**
 * BikeApi - interface
 *
 * @export
 * @interface BikeApiInterface
 */
export interface BikeApiInterface {
    /**
     *
     * @param {string} [id]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof BikeApiInterface
     */
    apiBikeDeleteRaw(
        requestParameters: ApiBikeDeleteRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<void>>;

    /**
     */
    apiBikeDelete(
        requestParameters: ApiBikeDeleteRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<void>;

    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof BikeApiInterface
     */
    apiBikeGetRaw(
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<Array<BikeResponse>>>;

    /**
     */
    apiBikeGet(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<BikeResponse>>;

    /**
     *
     * @param {string} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof BikeApiInterface
     */
    apiBikeIdGetRaw(
        requestParameters: ApiBikeIdGetRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<BikeResponse>>;

    /**
     */
    apiBikeIdGet(
        requestParameters: ApiBikeIdGetRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<BikeResponse>;

    /**
     *
     * @param {PostBikeRequest} postBikeRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof BikeApiInterface
     */
    apiBikePostRaw(
        requestParameters: ApiBikePostRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<void>>;

    /**
     */
    apiBikePost(
        requestParameters: ApiBikePostRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<void>;

    /**
     *
     * @param {PutBikeRequest} putBikeRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof BikeApiInterface
     */
    apiBikePutRaw(
        requestParameters: ApiBikePutRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<void>>;

    /**
     */
    apiBikePut(
        requestParameters: ApiBikePutRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<void>;
}

/**
 *
 */
export class BikeApi extends runtime.BaseAPI implements BikeApiInterface {
    /**
     */
    async apiBikeDeleteRaw(
        requestParameters: ApiBikeDeleteRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<void>> {
        const queryParameters: any = {};

        if (requestParameters["id"] != null) {
            queryParameters["id"] = requestParameters["id"];
        }

        const headerParameters: runtime.HTTPHeaders = {};

        let urlPath = `/api/Bike`;

        const response = await this.request(
            {
                path: urlPath,
                method: "DELETE",
                headers: headerParameters,
                query: queryParameters
            },
            initOverrides
        );

        return new runtime.VoidApiResponse(response);
    }

    /**
     */
    async apiBikeDelete(
        requestParameters: ApiBikeDeleteRequest = {},
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<void> {
        await this.apiBikeDeleteRaw(requestParameters, initOverrides);
    }

    /**
     */
    async apiBikeGetRaw(
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<Array<BikeResponse>>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        let urlPath = `/api/Bike`;

        const response = await this.request(
            {
                path: urlPath,
                method: "GET",
                headers: headerParameters,
                query: queryParameters
            },
            initOverrides
        );

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(BikeResponseFromJSON));
    }

    /**
     */
    async apiBikeGet(initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<BikeResponse>> {
        const response = await this.apiBikeGetRaw(initOverrides);
        return await response.value();
    }

    /**
     */
    async apiBikeIdGetRaw(
        requestParameters: ApiBikeIdGetRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<BikeResponse>> {
        if (requestParameters["id"] == null) {
            throw new runtime.RequiredError(
                "id",
                'Required parameter "id" was null or undefined when calling apiBikeIdGet().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        let urlPath = `/api/Bike/{id}`;
        urlPath = urlPath.replace(`{${"id"}}`, encodeURIComponent(String(requestParameters["id"])));

        const response = await this.request(
            {
                path: urlPath,
                method: "GET",
                headers: headerParameters,
                query: queryParameters
            },
            initOverrides
        );

        return new runtime.JSONApiResponse(response, (jsonValue) => BikeResponseFromJSON(jsonValue));
    }

    /**
     */
    async apiBikeIdGet(
        requestParameters: ApiBikeIdGetRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<BikeResponse> {
        const response = await this.apiBikeIdGetRaw(requestParameters, initOverrides);
        return await response.value();
    }

    /**
     */
    async apiBikePostRaw(
        requestParameters: ApiBikePostRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<void>> {
        if (requestParameters["postBikeRequest"] == null) {
            throw new runtime.RequiredError(
                "postBikeRequest",
                'Required parameter "postBikeRequest" was null or undefined when calling apiBikePost().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters["Content-Type"] = "application/json";

        let urlPath = `/api/Bike`;

        const response = await this.request(
            {
                path: urlPath,
                method: "POST",
                headers: headerParameters,
                query: queryParameters,
                body: PostBikeRequestToJSON(requestParameters["postBikeRequest"])
            },
            initOverrides
        );

        return new runtime.VoidApiResponse(response);
    }

    /**
     */
    async apiBikePost(
        requestParameters: ApiBikePostRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<void> {
        await this.apiBikePostRaw(requestParameters, initOverrides);
    }

    /**
     */
    async apiBikePutRaw(
        requestParameters: ApiBikePutRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<runtime.ApiResponse<void>> {
        if (requestParameters["putBikeRequest"] == null) {
            throw new runtime.RequiredError(
                "putBikeRequest",
                'Required parameter "putBikeRequest" was null or undefined when calling apiBikePut().'
            );
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters["Content-Type"] = "application/json";

        let urlPath = `/api/Bike`;

        const response = await this.request(
            {
                path: urlPath,
                method: "PUT",
                headers: headerParameters,
                query: queryParameters,
                body: PutBikeRequestToJSON(requestParameters["putBikeRequest"])
            },
            initOverrides
        );

        return new runtime.VoidApiResponse(response);
    }

    /**
     */
    async apiBikePut(
        requestParameters: ApiBikePutRequest,
        initOverrides?: RequestInit | runtime.InitOverrideFunction
    ): Promise<void> {
        await this.apiBikePutRaw(requestParameters, initOverrides);
    }
}
