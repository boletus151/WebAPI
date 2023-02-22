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
    using System;
    using System.Collections.Generic;

    public class ErrorResponse
    {
        public string? HttpCode
        {
            get;
            set;
        }

        public string? HttpMessage
        {
            get;
            set;
        }

        public string Message { get; set; }

        public IEnumerable<MoreInfoError>? MoreInformation
        {
            get;
            set;
        }
        public Exception Exception { get; internal set; }
        public string CustomMessage { get; internal set; }
    }

    public class MoreInfoError
    {
        public MoreInfoError(string field, string value)
        {
            this.Field = field;
            this.Value = value;
        }

        public string Field
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }
    }
}
