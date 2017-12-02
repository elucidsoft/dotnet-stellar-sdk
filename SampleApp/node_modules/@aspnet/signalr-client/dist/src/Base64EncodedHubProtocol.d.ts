import { IHubProtocol, HubMessage, ProtocolType } from "./IHubProtocol";
export declare class Base64EncodedHubProtocol implements IHubProtocol {
    private wrappedProtocol;
    constructor(protocol: IHubProtocol);
    readonly name: string;
    readonly type: ProtocolType;
    parseMessages(input: any): HubMessage[];
    writeMessage(message: HubMessage): any;
}
