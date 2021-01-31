#pragma once

#include "ulimits.h"

/**
* Dummy converting function from int to int, checking the LOW and HIGH limits. Used when T_SC_COORD is an int
*
* @param[in] iValue
* @return The converted value.
*/
inline int n_int(int iValue)
{
  return ((iValue >= HIGH_LIMIT) ? HIGH_LIMIT : ((iValue <= LOW_LIMIT) ? LOW_LIMIT : iValue));
}

/**
 * Converts internal units from double to integer limiting the value to LOW_LIMIT and HIGH_LIMIT.
 *
 * @param[in] dValue
 * @return The converted value.
 */
inline int n_int(double dValue)
{
  // Without following checks for integer overflow Tstqs revealed a number of differences on 9-AUG-2011
  // -> hidden problems in pre_odbxx, panel, voronoi, a.c., ...
  if (dValue >= HIGH_LIMIT) {
    return (HIGH_LIMIT);
  }
  if (dValue <= LOW_LIMIT) {
    return (LOW_LIMIT);
  }
  if (dValue < 0.0) {
    return ((int)(dValue - 0.5));
  }
  return ((int)(dValue + 0.5));
}

//! Round a value to a given unit with as little precision lost as possible.
template<class T, class U>
T roundT(U xVal)
{
  T rv = (T)xVal;
  if (rv - xVal <= -0.5) { // positive value rounded down too much
    rv++;
  }
  else if ((T)xVal - xVal >= 0.5) { // negative value rounded up too much
    rv--;
  }
  return (rv);
}

//! template specialization, roundT<int>() is a wrapper to n_int()
template<>
inline int roundT<int>(double dVal)
{
  return (n_int(dVal));
}
