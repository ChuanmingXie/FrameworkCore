// OEM common header

#pragma once

#ifndef GER2ODB_EXPORTS
#define GER2ODB_EXPORTS
#endif //GER2ODB_EXPORTS

#include <cstdio>

// ucam conversion api namespace
namespace ucam {
  //! class that is thrown as an exception on error
  class GER2ODB_EXPORTS C_ConvException {
  public:
    //! @return textual representation of the signalled error
    const char* getErrorDescription() const;
  }; //class C_ConvException

  enum E_PROGRESS_STATUS {
    PROGRESS_IDLE = 0,
    PROGRESS_IMPORT = 1,
    PROGRESS_EXPORT = 2,
    PROGRESS_ERROR = -1
  };

  //! @return last error (NULL on no error)
  GER2ODB_EXPORTS const char* LastError();

  /*! indicate an error and set error message
  * @param[in] szMsg message to store
  * @param[in] parameters
  */
  GER2ODB_EXPORTS void SetErrorMessage(const char* szMsg, ...);

  /*! Feedback reporting function
  *
  * NOTE: this function is supposed to be called from another thread and it will always return current done/total counters for current operation
  *
  * @param[inout] uDone done counter
  * @param[inout] uTotal total todo counter
  *
  * @return status
  */
  GER2ODB_EXPORTS E_PROGRESS_STATUS Progress(size_t& uDone, size_t& uTotal);

  /*! Initialize ucam engine, set ucam db, throw C_ConvException on error.
  *
  * @param[in] szUcamDbPath      path to the Ucam.db file
  * @param[in] szLicense         optional id of the alternative license
  * @param[in] xlogFileOut       optional output log FILE-stream, defaults to stdout
  */
  GER2ODB_EXPORTS void Initialize(const char* szUcamDbPath, const char* szLicense = NULL, FILE* xlogFileOut = stdout);

  /*! Initialize ucam engine, set ucam db, throw C_ConvException on error.
  *
  * @param[in] szUcamDbPath      path to the Ucam.db file
  * @param[in] szLicense         id of the alternative license
  * @param[in] szLog             log storage name (tempPath/name).
  */
  GER2ODB_EXPORTS void Initialize(const char* szUcamDbPath, const char* szLicense, const char* szLog);

  /*! Get ucam build info, such as githash, branch name, ...
  *
  * @return a string with all ucam.dll build info
  */
  GER2ODB_EXPORTS const char* BuildInfo();
} //namespace ucam
