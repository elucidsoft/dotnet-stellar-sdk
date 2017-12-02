import { IHttpConnectionOptions } from "./IHttpConnectionOptions";
import { IHubProtocol } from "./IHubProtocol";
export interface IHubConnectionOptions extends IHttpConnectionOptions {
    protocol?: IHubProtocol;
}
