import { IHubProtocol, ProtocolType, HubMessage } from "./IHubProtocol";
export declare class JsonHubProtocol implements IHubProtocol {
    readonly name: string;
    readonly type: ProtocolType;
    parseMessages(input: string): HubMessage[];
    writeMessage(message: HubMessage): string;
}
