using System;
using Jint.Native;
using Jint.Native.Error;
using Jint.Runtime.CallStack;

namespace Jint.Runtime
{
    public class JavaScriptException : Exception
    {
        private readonly JsValue _errorObject;

	    public JavaScriptException(ErrorConstructor errorConstructor) : base("")
        {
            _errorObject = errorConstructor.Construct(Arguments.Empty);
	        CallStack = errorConstructor.Engine.CallStack;
        }

        public JavaScriptException(ErrorConstructor errorConstructor, string message)
            : base(message)
        {
            _errorObject = errorConstructor.Construct(new JsValue[] { message });
			CallStack = errorConstructor.Engine.CallStack;
		}

        public JavaScriptException(JsValue error, JintCallStack callStack)
            : base(GetErrorMessage(error))
        {
            _errorObject = error;
	        CallStack = callStack;
        }

        private static string GetErrorMessage(JsValue error) 
        {
            if (error.IsObject())
            {
                var oi = error.AsObject();
                var message = oi.Get("message").AsString();
                return message;
            }
            else
                return string.Empty;            
        }

        public JsValue Error { get { return _errorObject; } }

        public override string ToString()
        {
            return _errorObject.ToString();
        }

        public Jint.Parser.Location Location { get; set; }

        public int LineNumber { get { return null == Location ? 0 : Location.Start.Line; } }

        public int Column { get { return null == Location ? 0 : Location.Start.Column; } }

		public JintCallStack CallStack { get; }
    }
}
