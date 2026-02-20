import {
    AuthApi,
    BikeApi,
    Configuration,
    type ConfigurationParameters,
    type HTTPHeaders,
    PartApi,
    TypeApi
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
    return new BikeApi(cfg);
}

export function getPartClient(): PartApi {
    const cfg = new Configuration(new CfgParams());
    return new PartApi(cfg);
}

export function getAuthClient(): AuthApi {
    const cfg = new Configuration(new CfgParams());
    return new AuthApi(cfg);
}

export function getTypeClient(): TypeApi {
    const cfg = new Configuration(new CfgParams());
    return new TypeApi(cfg);
}
