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

import { mapValues } from "../runtime";
/**
 *
 * @export
 * @interface EquipmentTypeResponse
 */
export interface EquipmentTypeResponse {
    /**
     *
     * @type {string}
     * @memberof EquipmentTypeResponse
     */
    id: string;
    /**
     *
     * @type {string}
     * @memberof EquipmentTypeResponse
     */
    name: string;
    /**
     *
     * @type {number}
     * @memberof EquipmentTypeResponse
     */
    category: number;
}

/**
 * Check if a given object implements the EquipmentTypeResponse interface.
 */
export function instanceOfEquipmentTypeResponse(value: object): value is EquipmentTypeResponse {
    if (!("id" in value) || value["id"] === undefined) return false;
    if (!("name" in value) || value["name"] === undefined) return false;
    if (!("category" in value) || value["category"] === undefined) return false;
    return true;
}

export function EquipmentTypeResponseFromJSON(json: any): EquipmentTypeResponse {
    return EquipmentTypeResponseFromJSONTyped(json, false);
}

export function EquipmentTypeResponseFromJSONTyped(json: any, ignoreDiscriminator: boolean): EquipmentTypeResponse {
    if (json == null) {
        return json;
    }
    return {
        id: json["id"],
        name: json["name"],
        category: json["category"]
    };
}

export function EquipmentTypeResponseToJSON(json: any): EquipmentTypeResponse {
    return EquipmentTypeResponseToJSONTyped(json, false);
}

export function EquipmentTypeResponseToJSONTyped(
    value?: EquipmentTypeResponse | null,
    ignoreDiscriminator: boolean = false
): any {
    if (value == null) {
        return value;
    }

    return {
        id: value["id"],
        name: value["name"],
        category: value["category"]
    };
}
