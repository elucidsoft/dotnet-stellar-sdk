using System;
using System.Linq;
using stellar_dotnet_sdk.xdr;

namespace stellar_dotnet_sdk;

/// <summary>
/// Base class for operations that invoke host functions.
/// </summary>
public abstract class InvokeHostFunctionOperation : Operation { }

/// <summary>
/// Operation that invokes a Soroban host function to invoke a contract
/// </summary>
public class InvokeContractOperation : InvokeHostFunctionOperation
{
    public InvokeContractOperation(InvokeContractHostFunction hostFunction)
    {
        HostFunction = hostFunction;
        Auth = Array.Empty<SorobanAuthorizationEntry>();
    }
    
    public InvokeContractHostFunction HostFunction { get; }
    public SorobanAuthorizationEntry[] Auth { get; private set; }

    /// <summary>
    /// Creates a new InvokeContractOperation object from the given base64-encoded XDR Operation.
    /// </summary>
    /// <param name="xdrBase64"></param>
    /// <returns>InvokeContractOperation object</returns>
    /// <exception cref="InvalidOperationException">Thrown when the base64-encoded XDR value is invalid.</exception>
    public static InvokeContractOperation FromOperationXdrBase64(string xdrBase64)
    {
        var bytes = Convert.FromBase64String(xdrBase64);
        var reader = new XdrDataInputStream(bytes);
        var thisXdr = xdr.InvokeHostFunctionOp.Decode(reader);
        if (thisXdr == null)
            throw new InvalidOperationException("Operation XDR is invalid");

        return FromInvokeHostFunctionOperationXdr(thisXdr);
    }

    public static InvokeContractOperation FromInvokeHostFunctionOperationXdr(xdr.InvokeHostFunctionOp xdrInvokeHostFunctionOp)
    {
        if (xdrInvokeHostFunctionOp.HostFunction.Discriminant.InnerValue != xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_INVOKE_CONTRACT)
            throw new InvalidOperationException("Invalid HostFunction type");

        return new InvokeContractOperation(
            hostFunction: InvokeContractHostFunction.FromHostFunctionXdr(xdrInvokeHostFunctionOp.HostFunction)
        )
        {
            Auth = xdrInvokeHostFunctionOp.Auth.Select(SorobanAuthorizationEntry.FromXdr).ToArray()
        };
    }

    public xdr.InvokeHostFunctionOp ToInvokeHostFunctionOperationXdr()
    {
        return new xdr.InvokeHostFunctionOp
        {
            HostFunction = new xdr.HostFunction
            {
                Discriminant = new xdr.HostFunctionType
                {
                    InnerValue = xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_INVOKE_CONTRACT
                },
                InvokeContract = HostFunction.ToXdr(),
            },
            Auth = Auth.Select(a => a.ToXdr()).ToArray()
        };
    }
    
    public override xdr.Operation.OperationBody ToOperationBody()
    {
        var body = new xdr.Operation.OperationBody
        {
            Discriminant = new xdr.OperationType
            {
                InnerValue = xdr.OperationType.OperationTypeEnum.INVOKE_HOST_FUNCTION
            },
            InvokeHostFunctionOp = ToInvokeHostFunctionOperationXdr()
        };
        return body;
    }

    public class Builder
    {
        private SCAddress? _contractAddress;
        private SCSymbol? _functionName;
        private SCVal[]? _args;
        private SorobanAuthorizationEntry[]? _auth;
    
        private KeyPair? _sourceAccount;

        public Builder()
        {
        }

        public Builder(xdr.InvokeHostFunctionOp operationXdr)
        {
            _contractAddress = SCAddress.FromXdr(operationXdr.HostFunction.InvokeContract.ContractAddress);
            _functionName = SCSymbol.FromXdr(operationXdr.HostFunction.InvokeContract.FunctionName);
            _args = operationXdr.HostFunction.InvokeContract.Args.Select(SCVal.FromXdr).ToArray();
            _auth = operationXdr.Auth.Select(SorobanAuthorizationEntry.FromXdr).ToArray();
        }

        public Builder(
            InvokeContractHostFunction hostFunction,
            SorobanAuthorizationEntry[]? auth = null)
        {
            _contractAddress = hostFunction.ContractAddress;
            _functionName = hostFunction.FunctionName;
            _args = hostFunction.Args;
            _auth = auth;
        }

        public Builder(
            SCAddress contractAddress,
            SCSymbol functionName,
            SCVal[] args,
            SorobanAuthorizationEntry[]? auth = null)
        {
            _contractAddress = contractAddress;
            _functionName = functionName;
            _args = args;
            _auth = auth;
        }
        
        public Builder SetContractAddress(SCAddress contractAddress)
        {
            _contractAddress = contractAddress;
            return this;
        }
        
        public Builder SetFunctionName(SCSymbol functionName)
        {
            _functionName = functionName;
            return this;
        }
        
        public Builder SetArgs(SCVal[] args)
        {
            _args = args;
            return this;
        }
        
        public Builder SetAuth(SorobanAuthorizationEntry[] auth)
        {
            _auth = auth;
            return this;
        }
        
        public Builder AddAuth(SorobanAuthorizationEntry auth)
        {
            _auth ??= Array.Empty<SorobanAuthorizationEntry>();
            _auth = _auth.Append(auth).ToArray();
            return this;
        }
        
        public Builder RemoveAuth(SorobanAuthorizationEntry auth)
        {
            if (_auth == null)
                return this;
            
            _auth = _auth.Where(a => !a.Equals(auth)).ToArray();
            return this;
        }

        public Builder SetSourceAccount(KeyPair sourceAccount)
        {
            _sourceAccount = sourceAccount;
            return this;
        }
    
        public InvokeContractOperation Build()
        {
            if (_contractAddress == null)
                throw new InvalidOperationException("Contract address cannot be null");
            if (_functionName == null)
                throw new InvalidOperationException("Function name cannot be null");

            var operation = new InvokeContractOperation(
                hostFunction: new InvokeContractHostFunction(
                    contractAddress: _contractAddress,
                    functionName: _functionName,
                    args: _args ?? Array.Empty<SCVal>()))
            {
                Auth = _auth ?? Array.Empty<SorobanAuthorizationEntry>(),
            };
            if (_sourceAccount != null)
            {
                operation.SourceAccount = _sourceAccount;
            }
            return operation;
        }
    }
}

public class CreateContractOperation : InvokeHostFunctionOperation
{
    public CreateContractOperation(CreateContractHostFunction hostFunction)
    {
        HostFunction = hostFunction;
        Auth = Array.Empty<SorobanAuthorizationEntry>();
    }
    
    public CreateContractHostFunction HostFunction { get; }
    public SorobanAuthorizationEntry[] Auth { get; private set; }

    /// <summary>
    /// Creates a new CreateContractOperation object from the given base64-encoded XDR Operation.
    /// </summary>
    /// <param name="xdrBase64"></param>
    /// <returns>CreateContractOperation object</returns>
    /// <exception cref="InvalidOperationException">Thrown when the base64-encoded XDR value is invalid.</exception>
    public static CreateContractOperation FromOperationXdrBase64(string xdrBase64)
    {
        var bytes = Convert.FromBase64String(xdrBase64);
        var reader = new XdrDataInputStream(bytes);
        var thisXdr = xdr.InvokeHostFunctionOp.Decode(reader);
        if (thisXdr == null)
            throw new InvalidOperationException("Operation XDR is invalid");

        return FromInvokeHostFunctionOperationXdr(thisXdr);
    }

    public static CreateContractOperation FromInvokeHostFunctionOperationXdr(xdr.InvokeHostFunctionOp xdrInvokeHostFunctionOp)
    {
        if (xdrInvokeHostFunctionOp.HostFunction.Discriminant.InnerValue != xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_CREATE_CONTRACT)
            throw new InvalidOperationException("Invalid HostFunction type");

        return new CreateContractOperation(
            hostFunction: CreateContractHostFunction.FromHostFunctionXdr(xdrInvokeHostFunctionOp.HostFunction)
        )
        {
            Auth = xdrInvokeHostFunctionOp.Auth.Select(SorobanAuthorizationEntry.FromXdr).ToArray()
        };
    }

    public xdr.InvokeHostFunctionOp ToInvokeHostFunctionOperationXdr()
    {
        return new xdr.InvokeHostFunctionOp
        {
            HostFunction = new xdr.HostFunction
            {
                Discriminant = new xdr.HostFunctionType
                {
                    InnerValue = xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_CREATE_CONTRACT
                },
                CreateContract = HostFunction.ToXdr(),
            },
            Auth = Auth.Select(a => a.ToXdr()).ToArray()
        };
    }
    
    public override xdr.Operation.OperationBody ToOperationBody()
    {
        var body = new xdr.Operation.OperationBody
        {
            Discriminant = new xdr.OperationType
            {
                InnerValue = xdr.OperationType.OperationTypeEnum.INVOKE_HOST_FUNCTION
            },
            InvokeHostFunctionOp = ToInvokeHostFunctionOperationXdr()
        };
        return body;
    }

    public class Builder
    {
        private ContractIDPreimage? _contractIDPreimage;
        private ContractExecutable? _executable;
        private SorobanAuthorizationEntry[]? _auth;

        private KeyPair? _sourceAccount;

        public Builder()
        {
        }

        public Builder(xdr.InvokeHostFunctionOp operationXdr)
        {
            _contractIDPreimage =
                ContractIDPreimage.FromXdr(operationXdr.HostFunction.CreateContract.ContractIDPreimage);
            _executable = ContractExecutable.FromXdr(operationXdr.HostFunction.CreateContract.Executable);
            _auth = operationXdr.Auth.Select(SorobanAuthorizationEntry.FromXdr).ToArray();
        }
        
        public Builder(
            CreateContractHostFunction hostFunction,
            SorobanAuthorizationEntry[]? auth = null)
        {
            _contractIDPreimage = hostFunction.ContractIDPreimage;
            _executable = hostFunction.Executable;
            _auth = auth;
        }

        public Builder(
            ContractIDPreimage contractIDPreimage,
            ContractExecutable executable,
            SorobanAuthorizationEntry[]? auth = null)
        {
            _contractIDPreimage = contractIDPreimage;
            _executable = executable;
            _auth = auth;
        }

        public Builder SetContractIDPreimage(ContractIDPreimage contractIDPreimage)
        {
            _contractIDPreimage = contractIDPreimage;
            return this;
        }

        public Builder SetExecutable(ContractExecutable executable)
        {
            _executable = executable;
            return this;
        }

        public Builder SetAuth(SorobanAuthorizationEntry[] auth)
        {
            _auth = auth;
            return this;
        }

        public Builder AddAuth(SorobanAuthorizationEntry auth)
        {
            _auth ??= Array.Empty<SorobanAuthorizationEntry>();
            _auth = _auth.Append(auth).ToArray();
            return this;
        }

        public Builder RemoveAuth(SorobanAuthorizationEntry auth)
        {
            if (_auth == null)
                return this;

            _auth = _auth.Where(a => !a.Equals(auth)).ToArray();
            return this;
        }

        public Builder SetSourceAccount(KeyPair sourceAccount)
        {
            _sourceAccount = sourceAccount;
            return this;
        }

        public CreateContractOperation Build()
        {
            if (_contractIDPreimage == null)
                throw new InvalidOperationException("Contract ID preimage cannot be null");
            if (_executable == null)
                throw new InvalidOperationException("Executable cannot be null");

            var operation = new CreateContractOperation(
                hostFunction: new CreateContractHostFunction(
                    contractIDPreimage: _contractIDPreimage,
                    executable: _executable))
            {
                Auth = _auth ?? Array.Empty<SorobanAuthorizationEntry>(),
            };
            if (_sourceAccount != null)
            {
                operation.SourceAccount = _sourceAccount;
            }
            return operation;
        }
    }
}

public class UploadContractOperation : InvokeHostFunctionOperation
{
    public UploadContractOperation(UploadContractHostFunction hostFunction)
    {
        HostFunction = hostFunction;
        Auth = Array.Empty<SorobanAuthorizationEntry>();
    }
    
    public UploadContractHostFunction HostFunction { get; }
    public SorobanAuthorizationEntry[] Auth { get; private set; }

    /// <summary>
    /// Creates a new UploadContractOperation object from the given base64-encoded XDR Operation.
    /// </summary>
    /// <param name="xdrBase64"></param>
    /// <returns>UploadContractOperation object</returns>
    /// <exception cref="InvalidOperationException">Thrown when the base64-encoded XDR value is invalid.</exception>
    public static UploadContractOperation FromOperationXdrBase64(string xdrBase64)
    {
        var operation = Operation.FromXdrBase64(xdrBase64);
        if (operation == null)
            throw new InvalidOperationException("Operation XDR is invalid");
        
        if (operation is not UploadContractOperation invokeHostFunctionOperation)
            throw new InvalidOperationException("Operation is not InvokeHostFunctionOperation");

        return invokeHostFunctionOperation;
    }
    
    public static UploadContractOperation FromInvokeHostFunctionOperationXdr(xdr.InvokeHostFunctionOp xdrInvokeHostFunctionOp)
    {
        if (xdrInvokeHostFunctionOp.HostFunction.Discriminant.InnerValue != xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_UPLOAD_CONTRACT_WASM)
            throw new InvalidOperationException("Invalid HostFunction type");

        return new UploadContractOperation(
            hostFunction: UploadContractHostFunction.FromHostFunctionXdr(xdrInvokeHostFunctionOp.HostFunction)
        )
        {
            Auth = xdrInvokeHostFunctionOp.Auth.Select(SorobanAuthorizationEntry.FromXdr).ToArray()
        };
    }
    
    public xdr.InvokeHostFunctionOp ToInvokeHostFunctionOperationXdr()
    {
        return new xdr.InvokeHostFunctionOp
        {
            HostFunction = new xdr.HostFunction
            {
                Discriminant = new xdr.HostFunctionType
                {
                    InnerValue = xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_UPLOAD_CONTRACT_WASM
                },
                Wasm = HostFunction.ToXdr(),
            },
            Auth = Auth.Select(a => a.ToXdr()).ToArray()
        };
    }
    
    public override xdr.Operation.OperationBody ToOperationBody()
    {
        var body = new xdr.Operation.OperationBody
        {
            Discriminant = new xdr.OperationType
            {
                InnerValue = xdr.OperationType.OperationTypeEnum.INVOKE_HOST_FUNCTION
            },
            InvokeHostFunctionOp = ToInvokeHostFunctionOperationXdr()
        };
        return body;
    }
    
    public class Builder
    {
        private byte[]? _wasm;
        private SorobanAuthorizationEntry[]? _auth;
    
        private KeyPair? _sourceAccount;

        public Builder()
        {
        }

        public Builder(xdr.InvokeHostFunctionOp operationXdr)
        {
            _wasm = operationXdr.HostFunction.Wasm;
            _auth = operationXdr.Auth.Select(SorobanAuthorizationEntry.FromXdr).ToArray();
        }
        
        public Builder(
            UploadContractHostFunction hostFunction,
            SorobanAuthorizationEntry[]? auth = null)
        {
            _wasm = hostFunction.Wasm;
            _auth = auth;
        }

        public Builder(
            byte[] wasm,
            SorobanAuthorizationEntry[]? auth = null)
        {
            _wasm = wasm;
            _auth = auth;
        }
        
        public Builder SetWasm(byte[] wasm)
        {
            _wasm = wasm;
            return this;
        }
        
        public Builder SetAuth(SorobanAuthorizationEntry[] auth)
        {
            _auth = auth;
            return this;
        }
        
        public Builder AddAuth(SorobanAuthorizationEntry auth)
        {
            _auth ??= Array.Empty<SorobanAuthorizationEntry>();
            _auth = _auth.Append(auth).ToArray();
            return this;
        }
        
        public Builder RemoveAuth(SorobanAuthorizationEntry auth)
        {
            if (_auth == null)
                return this;
            
            _auth = _auth.Where(a => !a.Equals(auth)).ToArray();
            return this;
        }

        public Builder SetSourceAccount(KeyPair sourceAccount)
        {
            _sourceAccount = sourceAccount;
            return this;
        }
    
        public UploadContractOperation Build()
        {
            if (_wasm == null)
                throw new InvalidOperationException("Wasm cannot be null");

            var operation = new UploadContractOperation(
                hostFunction: new UploadContractHostFunction(
                    wasm: _wasm))
            {
                Auth = _auth ?? Array.Empty<SorobanAuthorizationEntry>(),
            };
            if (_sourceAccount != null)
            {
                operation.SourceAccount = _sourceAccount;
            }
            return operation;
        }
    }
}

public abstract class HostFunction
{
    public xdr.HostFunction ToXdr()
    {
        return this switch
        {
            InvokeContractHostFunction invokeContractArgs => invokeContractArgs.ToHostFunctionXdr(),
            CreateContractHostFunction createContractArgs => createContractArgs.ToHostFunctionXdr(),
            UploadContractHostFunction uploadContractArgs => uploadContractArgs.ToHostFunctionXdr(),
            _ => throw new InvalidOperationException("Unknown HostFunction type")
        };
    }

    public static HostFunction FromXdr(xdr.HostFunction xdrHostFunction)
    {
        return xdrHostFunction.Discriminant.InnerValue switch
        {
            xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_INVOKE_CONTRACT => InvokeContractHostFunction.FromHostFunctionXdr(xdrHostFunction),
            xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_CREATE_CONTRACT => CreateContractHostFunction.FromHostFunctionXdr(xdrHostFunction),
            xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_UPLOAD_CONTRACT_WASM => UploadContractHostFunction.FromHostFunctionXdr(xdrHostFunction),
            _ => throw new InvalidOperationException("Unknown HostFunction type")
        };
    }
}

public class InvokeContractHostFunction : HostFunction
{
    public InvokeContractHostFunction(
        SCAddress contractAddress,
        SCSymbol functionName,
        SCVal[] args)
    {
        ContractAddress = contractAddress;
        FunctionName = functionName;
        Args = args;
    }
    
    public SCAddress ContractAddress { get; }
    public SCSymbol FunctionName { get; }
    public SCVal[] Args { get; }
    
    public static InvokeContractHostFunction FromXdr(xdr.InvokeContractArgs xdrInvokeContractArgs)
    {
        return new InvokeContractHostFunction(
            contractAddress: SCAddress.FromXdr(xdrInvokeContractArgs.ContractAddress),
            functionName: SCSymbol.FromXdr(xdrInvokeContractArgs.FunctionName),
            args: xdrInvokeContractArgs.Args.Select(SCVal.FromXdr).ToArray()
        );
    }
    
    public static InvokeContractHostFunction FromHostFunctionXdr(xdr.HostFunction xdrHostFunction)
    {
        if (xdrHostFunction.Discriminant.InnerValue != xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_INVOKE_CONTRACT)
            throw new InvalidOperationException("Invalid HostFunction type");

        return new InvokeContractHostFunction(
            contractAddress: SCAddress.FromXdr(xdrHostFunction.InvokeContract.ContractAddress),
            functionName: SCSymbol.FromXdr(xdrHostFunction.InvokeContract.FunctionName),
            args: xdrHostFunction.InvokeContract.Args.Select(SCVal.FromXdr).ToArray()
        );
    }
    
    public xdr.InvokeContractArgs ToXdr()
    {
        return new xdr.InvokeContractArgs
        {
            ContractAddress = ContractAddress.ToXdr(),
            FunctionName = FunctionName.ToXdr(),
            Args = Args.Select(a => a.ToXdr()).ToArray(),
        };
    }
    
    public xdr.HostFunction ToHostFunctionXdr()
    {
        return new xdr.HostFunction
        {
            Discriminant = new xdr.HostFunctionType
            {
                InnerValue = xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_INVOKE_CONTRACT
            },
            InvokeContract = ToXdr(),
        };
    }
}

public class CreateContractHostFunction : HostFunction
{
    public CreateContractHostFunction(
        ContractIDPreimage contractIDPreimage,
        ContractExecutable executable)
    {
        ContractIDPreimage = contractIDPreimage;
        Executable = executable;
    }

    public ContractIDPreimage ContractIDPreimage { get; }
    public ContractExecutable Executable { get; }
    
    public static CreateContractHostFunction FromXdr(xdr.CreateContractArgs xdrCreateContractArgs)
    {
        return new CreateContractHostFunction(
            contractIDPreimage: ContractIDPreimage.FromXdr(xdrCreateContractArgs.ContractIDPreimage),
            executable: ContractExecutable.FromXdr(xdrCreateContractArgs.Executable)
        );
    }
    
    public static CreateContractHostFunction FromHostFunctionXdr(xdr.HostFunction xdrHostFunction)
    {
        if (xdrHostFunction.Discriminant.InnerValue != xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_CREATE_CONTRACT)
            throw new InvalidOperationException("Invalid HostFunction type");

        return new CreateContractHostFunction(
            contractIDPreimage: ContractIDPreimage.FromXdr(xdrHostFunction.CreateContract.ContractIDPreimage),
            executable: ContractExecutable.FromXdr(xdrHostFunction.CreateContract.Executable)
        );
    }
    
    public xdr.CreateContractArgs ToXdr()
    {
        return new xdr.CreateContractArgs
        {
            ContractIDPreimage = ContractIDPreimage.ToXdr(),
            Executable = Executable.ToXdr()
        };
    }
    
    public xdr.HostFunction ToHostFunctionXdr()
    {
        return new xdr.HostFunction
        {
            Discriminant = new xdr.HostFunctionType
            {
                InnerValue = xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_CREATE_CONTRACT
            },
            CreateContract = ToXdr(),
        };
    }
}

public class UploadContractHostFunction : HostFunction
{
    public UploadContractHostFunction(byte[] wasm)
    {
        Wasm = wasm;
    }

    public byte[] Wasm { get; }
    
    public static UploadContractHostFunction FromHostFunctionXdr(xdr.HostFunction xdrHostFunction)
    {
        if (xdrHostFunction.Discriminant.InnerValue != xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_UPLOAD_CONTRACT_WASM)
            throw new InvalidOperationException("Invalid HostFunction type");

        return new UploadContractHostFunction(
            wasm: xdrHostFunction.Wasm
        );
    }
    
    public byte[] ToXdr()
    {
        return Wasm;
    }
    
    public xdr.HostFunction ToHostFunctionXdr()
    {
        return new xdr.HostFunction
        {
            Discriminant = new xdr.HostFunctionType
            {
                InnerValue = xdr.HostFunctionType.HostFunctionTypeEnum.HOST_FUNCTION_TYPE_UPLOAD_CONTRACT_WASM
            },
            Wasm = ToXdr(),
        };
    }
}

public class SorobanAuthorizationEntry
{
    public SorobanCredentials Credentials { get; set; }
    public SorobanAuthorizedInvocation RootInvocation { get; set; }
    
    public xdr.SorobanAuthorizationEntry ToXdr()
    {
        return new xdr.SorobanAuthorizationEntry
        {
            Credentials = Credentials.ToXdr(),
            RootInvocation = RootInvocation.ToXdr()
        };
    }
    
    public static SorobanAuthorizationEntry FromXdr(xdr.SorobanAuthorizationEntry xdr)
    {
        return new SorobanAuthorizationEntry
        {
            Credentials = SorobanCredentials.FromXdr(xdr.Credentials),
            RootInvocation = SorobanAuthorizedInvocation.FromXdr(xdr.RootInvocation)
        };
    }
}

public abstract class SorobanCredentials
{
    public xdr.SorobanCredentials ToXdr()
    {
        return this switch
        {
            SorobanSourceAccountCredentials sourceAccount => sourceAccount.ToSorobanCredentialsXdr(),
            SorobanAddressCredentials address => address.ToSorobanCredentialsXdr(),
            _ => throw new InvalidOperationException("Unknown SorobanCredentials type")
        };
    }
    
    public static SorobanCredentials FromXdr(xdr.SorobanCredentials xdrSorobanCredentials)
    {
        return xdrSorobanCredentials.Discriminant.InnerValue switch
        {
            xdr.SorobanCredentialsType.SorobanCredentialsTypeEnum.SOROBAN_CREDENTIALS_SOURCE_ACCOUNT => SorobanSourceAccountCredentials.FromSorobanCredentialsXdr(xdrSorobanCredentials),
            xdr.SorobanCredentialsType.SorobanCredentialsTypeEnum.SOROBAN_CREDENTIALS_ADDRESS => SorobanAddressCredentials.FromSorobanCredentialsXdr(xdrSorobanCredentials),
            _ => throw new InvalidOperationException("Unknown SorobanCredentials type")
        };
    }
}

public class SorobanSourceAccountCredentials : SorobanCredentials
{
    public static SorobanSourceAccountCredentials FromSorobanCredentialsXdr(xdr.SorobanCredentials xdrSorobanCredentials)
    {
        if (xdrSorobanCredentials.Discriminant.InnerValue != xdr.SorobanCredentialsType.SorobanCredentialsTypeEnum.SOROBAN_CREDENTIALS_SOURCE_ACCOUNT)
            throw new InvalidOperationException("Invalid SorobanCredentials type");

        return new SorobanSourceAccountCredentials();
    }
    
    public xdr.SorobanCredentials ToSorobanCredentialsXdr()
    {
        return new xdr.SorobanCredentials
        {
            Discriminant = new xdr.SorobanCredentialsType
            {
                InnerValue = xdr.SorobanCredentialsType.SorobanCredentialsTypeEnum.SOROBAN_CREDENTIALS_SOURCE_ACCOUNT
            },
        };
    }
}

public class SorobanAddressCredentials : SorobanCredentials
{ 
    public SCAddress Address { get; set; }
    public long Nonce { get; set; }
    public uint SignatureExpirationLedger { get; set; }
    public SCVal Signature { get; set; }
    
    public static SorobanAddressCredentials FromSorobanCredentialsXdr(xdr.SorobanCredentials xdrSorobanCredentials)
    {
        if (xdrSorobanCredentials.Discriminant.InnerValue != xdr.SorobanCredentialsType.SorobanCredentialsTypeEnum.SOROBAN_CREDENTIALS_ADDRESS)
            throw new InvalidOperationException("Invalid SorobanCredentials type");

        return new SorobanAddressCredentials
        {
            Address = SCAddress.FromXdr(xdrSorobanCredentials.Address.Address),
            Nonce = xdrSorobanCredentials.Address.Nonce.InnerValue,
            SignatureExpirationLedger = xdrSorobanCredentials.Address.SignatureExpirationLedger.InnerValue,
            Signature = SCVal.FromXdr(xdrSorobanCredentials.Address.Signature)
        };
    }
    
    public xdr.SorobanCredentials ToSorobanCredentialsXdr()
    {
        return new xdr.SorobanCredentials
        {
            Discriminant = new xdr.SorobanCredentialsType
            {
                InnerValue = xdr.SorobanCredentialsType.SorobanCredentialsTypeEnum.SOROBAN_CREDENTIALS_ADDRESS
            },
            Address = new xdr.SorobanAddressCredentials
            {
                Address = Address.ToXdr(),
                Nonce = new xdr.Int64
                {
                    InnerValue = Nonce
                },
                SignatureExpirationLedger = new xdr.Uint32
                {
                    InnerValue = SignatureExpirationLedger
                },
                Signature = Signature.ToXdr()
            }
        };
    }
}

public class SorobanAuthorizedInvocation
{
    public SorobanAuthorizedFunction Function { get; set; }
    public SorobanAuthorizedInvocation[] SubInvocations { get; set; }
    
    public xdr.SorobanAuthorizedInvocation ToXdr()
    {
        return new xdr.SorobanAuthorizedInvocation
        {
            Function = Function.ToXdr(),
            SubInvocations = SubInvocations.Select(i => i.ToXdr()).ToArray()
        };
    }
    
    public static SorobanAuthorizedInvocation FromXdr(xdr.SorobanAuthorizedInvocation xdr)
    {
        return new SorobanAuthorizedInvocation
        {
            Function = SorobanAuthorizedFunction.FromXdr(xdr.Function),
            SubInvocations = xdr.SubInvocations.Select(FromXdr).ToArray()
        };
    }
}

public abstract class SorobanAuthorizedFunction
{
    public xdr.SorobanAuthorizedFunction ToXdr()
    {
        return this switch
        {
            SorobanAuthorizedContractFunction contractFn => contractFn.ToSorobanAuthorizedFunctionXdr(),
            SorobanAuthorizedCreateContractFunction createContractHostFn => createContractHostFn.ToSorobanAuthorizedFunctionXdr(),
            _ => throw new InvalidOperationException("Unknown SorobanAuthorizedFunction type")
        };
    }
    
    public static SorobanAuthorizedFunction FromXdr(xdr.SorobanAuthorizedFunction xdrSorobanAuthorizedFunction)
    {
        return xdrSorobanAuthorizedFunction.Discriminant.InnerValue switch
        {
            xdr.SorobanAuthorizedFunctionType.SorobanAuthorizedFunctionTypeEnum.SOROBAN_AUTHORIZED_FUNCTION_TYPE_CONTRACT_FN => SorobanAuthorizedContractFunction.FromSorobanAuthorizedFunctionXdr(xdrSorobanAuthorizedFunction),
            xdr.SorobanAuthorizedFunctionType.SorobanAuthorizedFunctionTypeEnum.SOROBAN_AUTHORIZED_FUNCTION_TYPE_CREATE_CONTRACT_HOST_FN => SorobanAuthorizedCreateContractFunction.FromSorobanAuthorizedFunctionXdr(xdrSorobanAuthorizedFunction),
            _ => throw new InvalidOperationException("Unknown SorobanAuthorizedFunction type")
        };
    }
}

public class SorobanAuthorizedContractFunction : SorobanAuthorizedFunction
{
    public InvokeContractHostFunction HostFunction { get; set; }
    
    public static SorobanAuthorizedFunction FromSorobanAuthorizedFunctionXdr(xdr.SorobanAuthorizedFunction xdrSorobanAuthorizedFunction)
    {
        if (xdrSorobanAuthorizedFunction.Discriminant.InnerValue != xdr.SorobanAuthorizedFunctionType.SorobanAuthorizedFunctionTypeEnum.SOROBAN_AUTHORIZED_FUNCTION_TYPE_CONTRACT_FN)
            throw new InvalidOperationException("Invalid SorobanAuthorizedFunction type");

        return new SorobanAuthorizedContractFunction
        {
            HostFunction = InvokeContractHostFunction.FromXdr(xdrSorobanAuthorizedFunction.ContractFn)
        };
    }
    
    public xdr.SorobanAuthorizedFunction ToSorobanAuthorizedFunctionXdr()
    {
        return new xdr.SorobanAuthorizedFunction
        {
            Discriminant = new xdr.SorobanAuthorizedFunctionType
            {
                InnerValue = xdr.SorobanAuthorizedFunctionType.SorobanAuthorizedFunctionTypeEnum.SOROBAN_AUTHORIZED_FUNCTION_TYPE_CONTRACT_FN
            },
            ContractFn = HostFunction.ToXdr(),
        };
    }
}

public class SorobanAuthorizedCreateContractFunction : SorobanAuthorizedFunction
{
    public CreateContractHostFunction HostFunction { get; set; }
    
    public static SorobanAuthorizedFunction FromSorobanAuthorizedFunctionXdr(xdr.SorobanAuthorizedFunction xdrSorobanAuthorizedFunction)
    {
        if (xdrSorobanAuthorizedFunction.Discriminant.InnerValue != xdr.SorobanAuthorizedFunctionType.SorobanAuthorizedFunctionTypeEnum.SOROBAN_AUTHORIZED_FUNCTION_TYPE_CREATE_CONTRACT_HOST_FN)
            throw new InvalidOperationException("Invalid SorobanAuthorizedFunction type");

        return new SorobanAuthorizedCreateContractFunction
        {
            HostFunction = CreateContractHostFunction.FromXdr(xdrSorobanAuthorizedFunction.CreateContractHostFn)
        };
    }
    
    public xdr.SorobanAuthorizedFunction ToSorobanAuthorizedFunctionXdr()
    {
        return new xdr.SorobanAuthorizedFunction
        {
            Discriminant = new xdr.SorobanAuthorizedFunctionType
            {
                InnerValue = xdr.SorobanAuthorizedFunctionType.SorobanAuthorizedFunctionTypeEnum.SOROBAN_AUTHORIZED_FUNCTION_TYPE_CREATE_CONTRACT_HOST_FN
            },
            CreateContractHostFn = HostFunction.ToXdr(),
        };
    }
}

public abstract class ContractIDPreimage
{
    public xdr.ContractIDPreimage ToXdr()
    {
        return this switch
        {
            ContractIDAddressPreimage fromAddress => fromAddress.ToContractIDPreimageXdr(),
            ContractIDAssetPreimage fromAsset => fromAsset.ToContractIDPreimageXdr(),
            _ => throw new InvalidOperationException("Unknown ContractIDPreimage type")
        };
    }
    
    public static ContractIDPreimage FromXdr(xdr.ContractIDPreimage xdrContractIDPreimage)
    {
        return xdrContractIDPreimage.Discriminant.InnerValue switch
        {
            xdr.ContractIDPreimageType.ContractIDPreimageTypeEnum.CONTRACT_ID_PREIMAGE_FROM_ADDRESS => ContractIDAddressPreimage.FromContractIDPreimageXdr(xdrContractIDPreimage),
            xdr.ContractIDPreimageType.ContractIDPreimageTypeEnum.CONTRACT_ID_PREIMAGE_FROM_ASSET => ContractIDAssetPreimage.FromContractIDPreimageXdr(xdrContractIDPreimage),
            _ => throw new InvalidOperationException("Unknown ContractIDPreimage type")
        };
    }
}

public class ContractIDAddressPreimage : ContractIDPreimage
{
    public SCAddress Address { get; set; }
    public Uint256 Salt { get; set; }
    
    public static ContractIDPreimage FromContractIDPreimageXdr(xdr.ContractIDPreimage xdrContractIDPreimage)
    {
        if (xdrContractIDPreimage.Discriminant.InnerValue != xdr.ContractIDPreimageType.ContractIDPreimageTypeEnum.CONTRACT_ID_PREIMAGE_FROM_ADDRESS)
            throw new InvalidOperationException("Invalid ContractIDPreimage type");

        return new ContractIDAddressPreimage
        {
            Address = SCAddress.FromXdr(xdrContractIDPreimage.FromAddress.Address),
            Salt = xdrContractIDPreimage.FromAddress.Salt,
        };
    }
    
    public xdr.ContractIDPreimage ToContractIDPreimageXdr()
    {
        return new xdr.ContractIDPreimage
        {
            Discriminant = new xdr.ContractIDPreimageType
            {
                InnerValue = xdr.ContractIDPreimageType.ContractIDPreimageTypeEnum.CONTRACT_ID_PREIMAGE_FROM_ADDRESS
            },
            FromAddress = new xdr.ContractIDPreimage.ContractIDPreimageFromAddress
            {
                Address = Address.ToXdr(),
                Salt = Salt,
            }
        };
    }
}

public class ContractIDAssetPreimage : ContractIDPreimage
{
    public Asset Asset { get; set; }
    
    public static ContractIDPreimage FromContractIDPreimageXdr(xdr.ContractIDPreimage xdrContractIDPreimage)
    {
        if (xdrContractIDPreimage.Discriminant.InnerValue != xdr.ContractIDPreimageType.ContractIDPreimageTypeEnum.CONTRACT_ID_PREIMAGE_FROM_ASSET)
            throw new InvalidOperationException("Invalid ContractIDPreimage type");

        return new ContractIDAssetPreimage
        {
            Asset = Asset.FromXdr(xdrContractIDPreimage.FromAsset)
        };
    }
    
    public xdr.ContractIDPreimage ToContractIDPreimageXdr()
    {
        return new xdr.ContractIDPreimage
        {
            Discriminant = new xdr.ContractIDPreimageType
            {
                InnerValue = xdr.ContractIDPreimageType.ContractIDPreimageTypeEnum.CONTRACT_ID_PREIMAGE_FROM_ASSET
            },
            FromAsset = Asset.ToXdr()
        };
    }
}