#pragma once

#define _USE_MATH_DEFINES // to get M_PI etc.
#include <math.h>
#include <string>
#include "round.h" // for n_int() and also IS_64BIT_BUILD

/*--------------------------------------------------------------*/
//! T_INT64/T_UINT64 true 64bits integer number
// FIXME RHAL: use the C++11 standard types int64_t/uint64_t directly (after switch to VS2015+)
#ifdef _MSC_VER
typedef __int64 T_INT64;
typedef unsigned __int64 T_UINT64;
#else
typedef long long T_INT64;
typedef unsigned long long T_UINT64;
#endif //_MSC_VER

/*--------------------------------------------------------------*/
// mid value between a and b, computed in ints without overflow and distributing "x.5" results evenly (so mid(10, 21) -> 15 but mid(21, 10) -> 16)
inline int mid(int a, int b)
{
  return ((a >> 1) + (b >> 1) + (a & 1));
}

/*--------------------------------------------------------------*/
// degrees <-> radian conversions
inline double deg2rad(double angle)
{
  return (angle * M_PI / 180.0);
}

inline double rad2deg(double angle)
{
  return (angle * 180.0 / M_PI);
}

/*--------------------------------------------------------------*/
template<class T, class U> T roundT(U xVal);

/*--------------------------------------------------------------*/
//! The base point type, with templateable unit value
template<class T> struct T_TypePoint {
  T x; //! The x coordinate
  T y; //! The y coordinate

  // note that this class cannot have a default constructor (because it is used inside unions)

  // set point at (0,0)
  inline void reset()
  {
    x = y = 0;
  }

  inline void operator*=(double factor)
  {
    x = roundT<T>(factor * x);
    y = roundT<T>(factor * y);
  }

  inline void operator+=(const T_TypePoint<T>& p)
  {
    x += p.x;
    y += p.y;
  }

  inline void operator-=(const T_TypePoint<T>& p)
  {
    x -= p.x;
    y -= p.y;
  }

  // unary - for negation
  inline T_TypePoint<T> operator-() const
  {
    T_TypePoint<T> opposite = { -x, -y };
    return (opposite);
  }

  // polar coordinates
  inline double length() const
  {
    return (hypot(x, y));
  }

  inline double angle() const
  {
    return (atan2(y, x));
  }

  // provide perpendicular vector (same as rot(90))
  inline T_TypePoint<T> perp() const
  {
    T_TypePoint<T> perp = { -y, x };
    return (perp);
  }

  // return vector rotated by given angle
  inline T_TypePoint<T> rotate(double angle) const
  {
    double sina = sin(angle);
    double cosa = cos(angle);
    T_TypePoint<T> rot = { roundT<T>(x * cosa - y * sina), roundT<T>(x * sina + y * cosa) };
    return (rot);
  }

  // swap x and y coordinates (inplace)
  inline void swapXY()
  {
    std::swap(x, y);
  }

  //! @return string representation
  std::string to_string() const
  {
    return ("[" + std::to_string(x) + ", " + std::to_string(y) + "]");
  }
}; //struct T_TypePoint

/*--------------------------------------------------------------*/
typedef T_TypePoint<int>    point;  //!< a point with integer unit
typedef T_TypePoint<double> dpoint; //!< a point with double unit

/*--------------------------------------------------------------*/
// FIXME: convert to class operators
template<class T> inline T_TypePoint<T> operator+(const T_TypePoint<T>& p1, const T_TypePoint<T>& p2)
{
  T_TypePoint<T> p = { p1.x + p2.x, p1.y + p2.y };
  return (p);
}

template<class T> inline T_TypePoint<T> operator-(const T_TypePoint<T>& p1, const T_TypePoint<T>& p2)
{
  T_TypePoint<T> p = { p1.x - p2.x, p1.y - p2.y };
  return (p);
}

template<class T, class U> inline T_TypePoint<T> operator*(const T_TypePoint<T>& p1, U xFactor)
{
  T_TypePoint<T> p = { roundT<T>(p1.x * xFactor), roundT<T>(p1.y * xFactor) };
  return (p);
}

template<class T, class U> inline T_TypePoint<T> operator*(U xFactor, const T_TypePoint<T>& p1)
{
  T_TypePoint<T> p = { roundT<T>(p1.x * xFactor), roundT<T>(p1.y * xFactor) };
  return (p);
}

template<class T, class U> inline T_TypePoint<T> operator/(const T_TypePoint<T>& p1, U xFactor)
{
  T_TypePoint<T> p = { roundT<T>(p1.x / xFactor), roundT<T>(p1.y / xFactor) };
  return (p);
}

template<class T> inline bool operator!=(const T_TypePoint<T>& p1, const T_TypePoint<T>& p2)
{
  return ((p1.x != p2.x) || (p1.y != p2.y));
}

template<class T> inline bool operator==(const T_TypePoint<T>& p1, const T_TypePoint<T>& p2)
{
  return ((p1.x == p2.x) && (p1.y == p2.y));
}

/*--------------------------------------------------------------*/
//! convert a point from one unit to another
template<class T, class U> inline T_TypePoint<T> convertPointUnit(const T_TypePoint<U>& p)
{
  T_TypePoint<T> other = { roundT<T>(p.x), roundT<T>(p.y) };
  return (other);
}

/*--------------------------------------------------------------*/
//! a class making it possible to use a point as a key in std::unordered_map
class C_PointHash : public point {
public:
  C_PointHash(const point& p)
  : point(p)
  {
  }

  inline operator size_t() const
  {
#ifdef IS_64BIT_BUILD
  // 64-bit
  return (((size_t)y << 32) | (size_t)x);
#else
  // 32-bit
  return (x + y);
#endif //IS_64BIT_BUILD
  }
}; //class C_PointHash

/*--------------------------------------------------------------*/
namespace std {
  template<> struct hash<C_PointHash> {
    inline size_t operator()(const C_PointHash& k) const
    {
      return ((size_t)k);
    }
  };
} //namespace std

/*--------------------------------------------------------------*/
//! The base line type, with templateable unit value
template<class T> struct T_TypeLine {
  T_TypePoint<T> from;
  T_TypePoint<T> to;

  // reset line to be empty
  inline void reset()
  {
    from.x = from.y = to.x = to.y = 0;
  }

  // return length of the line
  inline double length() const
  {
    return ((to - from).length());
  }

  // return midpoint of the line
  inline T_TypePoint<T> midpoint() const
  {
    return (0.5 * (from + to));
  }

  // return a point on the line (factor = 0.0 -> start point, 0.5 -> mid point, 1.0 -> end point)
  inline T_TypePoint<T> interpolate(double factor) const
  {
    double x = (1.0 - factor) * from.x + factor * to.x;
    double y = (1.0 - factor) * from.y + factor * to.y;
    T_TypePoint<T> p = { roundT<T>(x), roundT<T>(y) };
    return (p);
  }

  // shift the line by the given offset
  inline void move(const T_TypePoint<T>& off)
  {
    from += off;
    to += off;
  }
}; //struct T_TypeLine

/*--------------------------------------------------------------*/
typedef T_TypeLine<int>     line;   //!< a line with integer unit
typedef T_TypeLine<double>  dline;  //!< a line with double unit

/*--------------------------------------------------------------*/
// rectangle
struct rectangle {
  int left;
  int bot;
  int rite;
  int top;

  //! construct an invalid rectangle
  rectangle()
  {
    invalidate();
  }

  //! construct a rectangle with the given corners
  rectangle(int xmin, int ymin, int xmax, int ymax)
  : left(xmin)
  , bot(ymin)
  , rite(xmax)
  , top(ymax)
  {
  }

  //! construct a rectangle as an expansion of another one
  rectangle(const rectangle& rect, int exp)
  : left(rect.left - exp)
  , bot(rect.bot - exp)
  , rite(rect.rite + exp)
  , top(rect.top + exp)
  {
  }

  //! construct a rectangle around the given point (with eventual expansion)
  rectangle(const point& p, int exp = 0)
  {
    init(p);
    expand(exp);
  }

  //! construct a rectangle enclosing two points
  rectangle(const point& p1, const point& p2)
  {
    init(p1);
    add(p2);
  }

  //! is the given rectangle same as the current one?
  inline bool operator==(const rectangle& rect) const
  {
    return ((rect.left == left) && (rect.bot == bot) && (rect.rite == rite) && (rect.top == top));
  }

  //! is the given rectangle different from the current one?
  inline bool operator!=(const rectangle& rect) const
  {
    return ((rect.left != left) || (rect.bot != bot) || (rect.rite != rite) || (rect.top != top));
  }

  //! re-initialize the rectangle
  inline void init(int xmin, int ymin, int xmax, int ymax)
  {
    left = xmin;
    bot = ymin;
    rite = xmax;
    top = ymax;
  }

  //! re-initialize the rectangle
  inline void init(const point& p)
  {
    init(p.x, p.y, p.x, p.y);
  }

  //! invalidate the rectangle
  inline void invalidate()
  {
    init(HIGH_LIMIT, HIGH_LIMIT, LOW_LIMIT, LOW_LIMIT);
  }

  //! is the rectangle valid?
  inline bool is_valid() const
  {
    return ((rite >= left) && (top >= bot));
  }

  //! get width of the rectangle (at least 1 for a valid rectangle)
  inline int width() const
  {
    return (rite - left + 1);
  }

  //! get height of the rectangle (at least 1 for a valid rectangle)
  inline int height() const
  {
    return (top - bot + 1);
  }

  //! get area of the rectangle (as int_64t due to eventual overflow in standard int)
  // return -1 for invalid rectanges and 0 for rectangles which have at least one size zero
  inline T_INT64 area() const
  {
    T_INT64 w = (T_INT64)rite - (T_INT64)left;
    T_INT64 h = (T_INT64)top - (T_INT64)bot;
    if ((w < 0) || (h < 0)) {
      return (-1);
    }
    return (w * h);
  }

  //! get bottom left point of the rectangle
  inline point botleft() const
  {
    point p = { left, bot };
    return (p);
  }

  //! get bottom right point of the rectangle
  inline point botright() const
  {
    point p = { rite, bot };
    return (p);
  }

  //! get top left point of the rectangle
  inline point topleft() const
  {
    point p = { left, top };
    return (p);
  }

  //! get top right point of the rectangle
  inline point topright() const
  {
    point p = { rite, top };
    return (p);
  }

  //! get center of the rectangle
  inline point midpoint() const
  {
    point p = { mid(left, rite), mid(bot, top) };
    return (p);
  }

  //! expand the rectangle by the given amount
  inline void expand(int ex, int ey)
  {
    left -= ex;
    bot -= ey;
    rite += ex;
    top += ey;
  }

  //! expand the rectangle by the given amount
  inline void expand(int exp)
  {
    expand(exp, exp);
  }

  //! move (offset) the rectangle by the given amount
  inline void move(const point& delta)
  {
    int dx = delta.x;
    int dy = delta.y;
    left += dx;
    bot += dy;
    rite += dx;
    top += dy;
  }

  //! provide a copy of this rectangle moved by the given delta
  inline rectangle moved(const point& delta) const
  {
    int dx = delta.x;
    int dy = delta.y;
    return (rectangle(left + dx, bot + dy, rite + dx, top + dy));
  }

  //! add point to the rectangle (i.e. extend the rectangle so it covers the point)
  inline void add(const point& p)
  {
    int x = p.x;
    if (x < left) {
      left = x;
    }
    if (x > rite) {
      rite = x;
    }
    int y = p.y;
    if (y < bot) {
      bot = y;
    }
    if (y > top) {
      top = y;
    }
  }

  //! add rectangle to the existing rectangle (union)
  inline void add(const rectangle& rect)
  {
    if (rect.left < left) {
      left = rect.left;
    }
    if (rect.rite > rite) {
      rite = rect.rite;
    }
    if (rect.bot < bot) {
      bot = rect.bot;
    }
    if (rect.top > top) {
      top = rect.top;
    }
  }

  //! intersect with the given rectangle
  inline void intersect(const rectangle& rect)
  {
    if (rect.left > left) {
      left = rect.left;
    }
    if (rect.bot > bot) {
      bot = rect.bot;
    }
    if (rect.rite < rite) {
      rite = rect.rite;
    }
    if (rect.top < top) {
      top = rect.top;
    }
  }

  //! is the given point inside this rectangle?
  inline bool contains(const point& p) const
  {
    int x = p.x;
    int y = p.y;
    return ((x >= left) && (x <= rite) && (y >= bot) && (y <= top));
  }

  //! is the given rectangle inside this one?
  inline bool contains(const rectangle& rect) const
  {
    return ((rect.left >= left) && (rect.rite <= rite) && (rect.bot >= bot) && (rect.top <= top));
  }

  //! has the given rectangle non-empty intersection with this one?
  inline bool has_intersection(const rectangle& rect) const
  {
    return ((rect.left <= rite) && (rect.rite >= left) && (rect.bot <= top) && (rect.top >= bot));
  }
}; //struct rectangle

/*--------------------------------------------------------------*/
// because points are used in unions, they cannot have non-default contstructors. So provide constructor-like function instead
inline point create_point(int x, int y)
{
  point p = { x, y };
  return (p);
}

inline point create_point(double x, double y)
{
  point p = { n_int(x), n_int(y) };
  return (p);
}

inline point create_point(const dpoint& dp)
{
  return (create_point(dp.x, dp.y));
}

inline dpoint create_dpoint(double x, double y)
{
  dpoint p = { x, y };
  return (p);
}

inline dpoint create_dpoint(const point& p)
{
  return (create_dpoint(p.x, p.y));
}

/*--------------------------------------------------------------*/
// pre-defined points (in err_handling.c)
extern point ZERO_POINT;  // point at (0,0)
extern point LOW_POINT;   // point at (LOW_LIMIT, LOW_LIMIT)
extern point HIGH_POINT;  // point at (HIGH_LIMIT, HIGH_LIMIT)

/*--------------------------------------------------------------*/
// ditto for lines and dlines
inline line create_line(const point& from, const point& to)
{
  line l = { from, to };
  return (l);
}

inline dline create_dline(const dpoint& from, const dpoint& to)
{
  dline l = { from, to };
  return (l);
}

/*--------------------------------------------------------------*/
// midpoint between two points
inline point mid_point(const point& p1, const point& p2)
{
  point p = { mid(p1.x, p2.x), mid(p1.y, p2.y) };
  return (p);
}

inline dpoint mid_point(const dpoint& p1, const dpoint& p2)
{
  dpoint dp = { 0.5 * (p1.x + p2.x), 0.5 * (p1.y + p2.y) };
  return (dp);
}
