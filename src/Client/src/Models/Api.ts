import {
    Configuration,
    type ConfigurationParameters,
    type HTTPHeaders,
    BikeApi,
    PartApi,
    TypeApi,
    AuthApi
} from "../Gen";
import { loadBearerTokenFromLocalStorage } from "./Auth/User.ts";

class CfgParams implements ConfigurationParameters {
    basePath?: string | undefined;

    get headers(): HTTPHeaders | undefined {
        const bearerToken = loadBearerTokenFromLocalStorage();
        return {
            Authorization: `Bearer ${bearerToken}`
        };
    }

    constructor() {
        this.basePath = "";
    }
}

export function getBikeClient(): BikeApi {
    const cfg = new Configuration(new CfgParams());
    const api = new BikeApi(cfg);
    return api;
}
export function getPartClient(): PartApi {
    const cfg = new Configuration(new CfgParams());
    const api = new PartApi(cfg);
    return api;
}
export function getAuthClient(): AuthApi {
    const cfg = new Configuration(new CfgParams());
    const api = new AuthApi(cfg);
    return api;
}
export function getTypeClient(): TypeApi {
    const cfg = new Configuration(new CfgParams());
    const api = new TypeApi(cfg);
    return api;
}
