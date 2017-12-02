export declare namespace TextMessageFormat {
    function write(output: string): string;
    function parse(input: string): string[];
}
export declare namespace BinaryMessageFormat {
    function write(output: Uint8Array): ArrayBuffer;
    function parse(input: ArrayBuffer): Uint8Array[];
}
