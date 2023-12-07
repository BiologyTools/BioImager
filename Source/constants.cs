using System;

namespace BioImager
{
    static class Prior
    {
        /* equivalent of errors.h in C-SDK */
        /* *************************************************************************************************************************** */

        /**
         * @brief SDK ERROR CODES returned directly by the SDk 
         */
        public const int PRIOR_OK = 0;
        public const int PRIOR_UNRECOGNISED_COMMAND = -10001;
        public const int PRIOR_FAILEDTOOPENPORT = -10002;
        public const int PRIOR_FAILEDTOFINDCONTROLLER = -10003;
        public const int PRIOR_NOTCONNECTED = -10004;
        public const int PRIOR_ALREADYCONNECTED = -10005;
        public const int PRIOR_INVALID_PARAMETERS = -10007;
        public const int PRIOR_UNRECOGNISED_DEVICE = -10008;
        public const int PRIOR_APPDATAPATHERROR = -10009;
        public const int PRIOR_LOADERERROR = -10010;
        public const int PRIOR_CONTROLLERERROR = -10011;
        public const int PRIOR_NOTIMPLEMENTEDYET = -10012;
        public const int PRIOR_UNEXPECTED_ERROR = -10100;
        public const int PRIOR_SDK_NOT_INITIALISED = -10200;
        public const int PRIOR_SDK_INVALID_SESSION = -10300;
        public const int PRIOR_SDK_NOMORE_SESSIONS = -10301;


        /* *************************************************************************************************************************** */

        /**
         * @brief  CONTROLLER ERROR codes returned by the stage controller being used. 
         */
        public const int PRIOR_NO_STAGE = 1;
        public const int PRIOR_NOT_IDLE = 2;
        public const int PRIOR_NO_DRIVE = 3;
        public const int PRIOR_STRING_PARSE = 4;
        public const int PRIOR_COMMAND_NOT_FOUND = 5;
        public const int PRIOR_INVALID_SHUTTER = 6;
        public const int PRIOR_NO_FOCUS = 7;
        public const int PRIOR_VALUE_OUT_OF_RANGE = 8;
        public const int PRIOR_INVALID_WHEEL = 9;
        public const int PRIOR_ARG1_OUT_OF_RANGE = 10;
        public const int PRIOR_ARG2_OUT_OF_RANGE = 11;
        public const int PRIOR_ARG3_OUT_OF_RANGE = 12;
        public const int PRIOR_ARG4_OUT_OF_RANGE = 13;
        public const int PRIOR_ARG5_OUT_OF_RANGE = 14;
        public const int PRIOR_ARG6_OUT_OF_RANGE = 15;
        public const int PRIOR_INCORRECT_STATE = 16;
        public const int PRIOR_NO_FILTER_WHEEL = 17;
        public const int PRIOR_QUEUE_FULL = 18;
        public const int PRIOR_COMP_MODE_SET = 19;
        public const int PRIOR_SHUTTER_NOT_FITTED = 20;
        public const int PRIOR_INVALID_CHECKSUM = 21;
        public const int PRIOR_NOT_ROTARY = 22;
        public const int PRIOR_NO_FOURTH_AXIS = 40;
        public const int PRIOR_AUTOFOCUS_IN_PROG = 41;
        public const int PRIOR_NO_VIDEO = 42;
        public const int PRIOR_NO_ENCODER = 43;
        public const int PRIOR_SIS_NOT_DONE = 44;
        public const int PRIOR_NO_VACUUM_DETECTOR = 45;
        public const int PRIOR_NO_SHUTTLE = 46;
        public const int PRIOR_VACUUM_QUEUED = 47;
        public const int PRIOR_SIZ_NOT_DONE = 48;
        public const int PRIOR_NOT_SLIDE_LOADER = 49;
        public const int PRIOR_ALREADY_PRELOADED = 50;
        public const int PRIOR_STAGE_NOT_MAPPED = 51;
        public const int PRIOR_TRIGGER_NOT_FITTED = 52;
        public const int PRIOR_INTERPOLATOR_NOT_FITTED = 53;
        public const int PRIOR_WRITE_FAIL = 80;
        public const int PRIOR_ERASE_FAIL = 81;
        public const int PRIOR_NO_DEVICE = 128;
        public const int PRIOR_NO_PMD_AXIS = 129;

    };
};
