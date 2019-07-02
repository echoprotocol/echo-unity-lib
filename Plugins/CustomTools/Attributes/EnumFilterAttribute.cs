using CustomTools.Extensions.Core.Array;
using UnityEngine;


namespace CustomTools.Attributes {

    public class EnumFilterAttribute : PropertyAttribute {

        public System.Enum[] Filter { get; private set; }

        public EnumFilterAttribute( params object[] values ) {
            Filter = (values ?? new object[ 0 ]).ConvertAll( value => ( System.Enum )value );
        }
	}
}