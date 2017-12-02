import { IHubProtocol, ProtocolType, HubMessage } from "./IHubProtocol";
export declare class MessagePackHubProtocol implements IHubProtocol {
    readonly name: string;
    readonly type: ProtocolType;
    parseMessages(input: ArrayBuffer): HubMessage[];
    private parseMessage(input);
    private createInvocationMessage(properties);
    private createStreamItemMessage(properties);
    private createCompletionMessage(properties);
    writeMessage(message: HubMessage): ArrayBuffer;
    private writeInvocation(invocationMessage);
}
