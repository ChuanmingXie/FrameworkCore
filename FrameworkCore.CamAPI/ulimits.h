#pragma once

#define LOW_LIMIT  (-2000000000)
#define HIGH_LIMIT  (2000000000)

/*--------------------------------------------------------------*/
// do we compile in 64-bit or 32-bit mode?
#if defined(_WIN64) || (defined(INTPTR_MAX) && (INTPTR_MAX == INT64_MAX))
#define IS_64BIT_BUILD
#else
#undef IS_64BIT_BUILD
#endif //_WIN64, INTPTR_MAX, INT64_MAX

/*--------------------------------------------------------------*/
// technologies in development (to be defaulted after verified)
#define FIX_ATTR_NAMES // fix attribute names before save (RM1472 and RM1563, RHAL, 28.2.19, 28.3.19)
//#define DIRTY_DDB_BBOXES // manage bboxes in ddbs via dirty flag (LUBR, 26.5.20, disabled as still not working properly)
