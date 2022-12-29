// -------------------------------------------------------------------------------------------------------------------
//  <summary>
//     This implementation is a group of the offers of several persons along the network;
//     because of this, it is under Creative Common By License:
//
//     You are free to:
//
//     Share — copy and redistribute the material in any medium or format
//     Adapt — remix, transform, and build upon the material for any purpose, even commercially.
//
//     The licensor cannot revoke these freedoms as long as you follow the license terms.
//
//     Under the following terms:
//
//     Attribution — You must give appropriate credit, provide a link to the license, and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
//     No additional restrictions — You may not apply legal terms or technological measures that legally restrict others from doing anything the license permits.
//
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------

namespace WebAPI_BaseComponents.Responses
{
    using System.Collections.Generic;
    using WebAPI_BaseComponents.Responses;

    /// <summary>
    ///     HttpResponseCustom class.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        ///     Código del error
        /// </summary>
        public string? HttpCode
        {
            get;
            set;
        }

        /// <summary>
        ///     Mensaje del error
        /// </summary>
        public string? HttpMessage
        {
            get;
            set;
        }

        /// <summary>
        ///     Información adicional
        /// </summary>
        public IEnumerable<MoreInfoError>? MoreInformation
        {
            get;
            set;
        }
    }

    /// <summary>
    ///     Clase que refleja el detalle del error
    /// </summary>
    public class MoreInfoError
    {
        /// <summary>
        ///     Clave
        /// </summary>
        public string? Campo
        {
            get;
            set;
        }

        /// <summary>
        ///     Valor del error
        /// </summary>
        public string? Valor
        {
            get;
            set;
        }
    }
}
