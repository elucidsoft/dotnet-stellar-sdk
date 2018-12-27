using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using stellar_dotnet_sdk.requests;

namespace stellar_dotnet_sdk
{
    /// <summary>
    /// Partial implement of the Uri Template Spec (RFC 6570).
    ///
    /// In particular it implements:
    ///
    ///  * Query component expressions ( {?var1,var2,...,varn} )
    ///  * Simple variables ( {var} )
    /// </summary>
    internal class UriTemplate
    {
        private readonly string _template;

        private enum States
        {
            COPYING_LITERALS,
            PARSING_EXPRESSION
        }

        public UriTemplate(string template)
        {
            _template = template;
        }

        public Uri Resolve(object parameters)
        {
            if (parameters is null)
            {
                return Resolve();
            }

            var parametersDict = new Dictionary<string, object>();
            var properties = parameters.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in properties)
            {
                parametersDict[propertyInfo.Name] = propertyInfo.GetValue(parameters, null);
            }

            return Resolve(parametersDict);
        }

        public Uri Resolve(IDictionary<string, object> parameters = null)
        {
            var currentState = States.COPYING_LITERALS;
            var resultBuilder = new StringBuilder();
            var expressionBuilder = new StringBuilder();

            foreach (var character in _template.ToCharArray())
            {
                switch (currentState)
                {
                    case States.COPYING_LITERALS:
                        if (character == '{')
                        {
                            currentState = States.PARSING_EXPRESSION;
                            expressionBuilder.Clear();
                        }
                        else if (character == '}')
                        {
                            throw new ArgumentException("Malformed template, unexpected }");
                        }
                        else
                        {
                            resultBuilder.Append(character);
                        }

                        break;
                    case States.PARSING_EXPRESSION:
                        if (character == '}')
                        {
                            ProcessExpression(expressionBuilder, resultBuilder, parameters);
                            currentState = States.COPYING_LITERALS;
                        }
                        else
                        {
                            expressionBuilder.Append(character);
                        }

                        break;
                }
            }

            if (currentState == States.PARSING_EXPRESSION)
            {
                throw new ArgumentException("Malformed template, expecting }");
            }

            return new Uri(resultBuilder.ToString());
        }

        private void ProcessExpression(StringBuilder expression, StringBuilder result,
            IDictionary<string, object> parameters)
        {
            if (parameters == null) return;
            if (expression.Length == 0)
            {
                throw new ArgumentException("Malformed template {}");
            }

            var isQueryParameter = expression[0] == '?';
            var isFirst = true;
            var firstIndex = isQueryParameter ? 1 : 0;
            var varName = new StringBuilder();

            for (int i = firstIndex; i < expression.Length; i++)
            {
                var currentChar = expression[i];
                switch (currentChar)
                {
                    case ',':
                        ProcessVariable(varName.ToString(), isQueryParameter, isFirst, result, parameters);
                        varName.Clear();
                        isFirst = false;
                        break;
                    default:
                        varName.Append(currentChar);
                        break;
                }
            }
            ProcessVariable(varName.ToString(), isQueryParameter, isFirst, result, parameters);
        }

        private void ProcessVariable(string varName, bool isQueryParameter, bool isFirst, StringBuilder result,
            IDictionary<string, object> parameters)
        {
            if (parameters == null) return;
            if (!parameters.ContainsKey(varName)) return;

            if (isFirst)
            {
                if (isQueryParameter)
                {
                    result.Append('?');
                }
            }
            else
            {
                if (isQueryParameter)
                {
                    result.Append('&');
                }
                else
                {
                    result.Append(',');
                }
            }

            if (isQueryParameter)
            {
                result.Append(varName);
                result.Append("=");
            }

            var value = parameters[varName];

            if (value is string stringValue)
            {
                result.Append(stringValue);
            }
            else if (value is OrderDirection order)
            {
                result.Append(order.ToString().ToLower());
            }
            else
            {
                result.Append(value.ToString());
            }
        }
    }
}