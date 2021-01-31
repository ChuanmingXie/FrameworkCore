// Draft ODBxx to gdsII or gerber conversion API for CFMEE
#pragma once
#include "oem.h"

// ucam conversion api namespace
namespace ucam {
  //! job proxy class
  class  GER2ODB_EXPORTS C_CFMEE {
    const char** pSteps_; //!< list of steps from an odb++ file
    const char** pLayers_; //!< list of layers from an odb++ file
    const char** pLoadedLayers_; //!< list of loaded layers
    const char* szLastError_; //!< the last error message

    /*! indicate an error and set error message
    * @param[in] szMsg message to store
    * @param[in] parameters
    */
    void setErrorMessage(const char* szMsg, ...);

  public:
    /*! create internal structures for an empty job
    *
    * @param[in] szJobName name of the job
    */
    C_CFMEE(const char* szJobName);

    /*! add dpf file as signal layer to the job (to the bottom of the job)
    *
    * @param[in] szFilePath path the the 274X file (filename wildcards are accepted)
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool loadDpf(const char* szFilePath);

    /*! add 274X file as signal layer to the job (to the bottom of the job)
    *
    * @param[in] szFilePath path the the 274X file (filename wildcards are accepted)
    * @param[in] szLayName name of the layer, NULL take file specified name as the layer name
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool import274X(const char* szFilePath, const char* szLayName = NULL);

    /*! add 274 file as signal layer to the job (to the bottom of the job)
    *
    * @param[in] szFilePath path the the gerber file (filename wildcards are accepted)
    * @param[in] szWheel path the the wheel file
    * @param[in] szLayName name of the layer, NULL take file specified name as the layer name
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool importGer(const char* szFilePath, const char* szWheel, const char* szLayName = NULL);

    /*! import ODB++ step/layer
    *
    * @param[in] szOdbPath path the the ODB++ root directory
    * @param[in] szOdbJob Jobname
    * @param[in] szStep stepname
    * @param[in] szOdbLayer layer name
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool importODBStep(const char* szOdbPath, const char* szOdbJob, const char* szOdbStep, const char* szOdbLayer);

    /*! get ODB++ step info
    *
    * @param[in] szOdbPath path the the ODB++ root directory
    * @param[in] szOdbJob Jobname
    *
    * @return NULL-terminated list of steps in the ODB++ data structure (the structure is allocated locally inside C_Job instance, it is released when destructing it
    */
    const char** getODBStepList(const char* szOdbPath, const char* szOdbJob);

    /*! get ODB++ layer info
    *
    * @param[in] szOdbPath path the the ODB++ root directory
    * @param[in] szOdbJob Jobname
    *
    * @return NULL-terminated list of layers in the ODB++ data structure (the structure is allocated locally inside C_Job instance, it is released when destructing it
    */
    const char** getODBLayerList(const char* szOdbPath, const char* szOdbJob);

    /*! set the profile layer to use.
    * If szODBProfile is NULL or "profile", the default ODB profile will be used.
    *
    * @param[in] szODBProfileName the profile to use
    */
    void setODBProfile(const char* szODBProfileName);

    /*! set transformation for the next imported layer
    *
    * @param[in] bFlipX mirror on X if true
    * @param[in] bFlipY mirror on Y if true
    * @param[in] fRotation layer rotation (allowed values 270, 180, 90 or 0)
    * @param[in] bReverse reverse the loaded layer
    * @param[in] szUnits "mm", mil", "inch" units for distort parameters
    * @param[in] fShiftX horizontal shift for imported layer
    * @param[in] fShiftY vertical shift for imported layer
    * @param[in] bContourize set if the output should be contourized or not
    * @param[in] fDistortX - factor for scale in the axe X
    * @param[in] fDistortY - factor for scale in the axe Y
    * @param[in] bDeleteOutside delete objects outside the profile layer if true
    * @param[in] dMarginX the margin X (+/-) to clip based on the profile layer, in szUnits
    * @param[in] dMarginY the margin Y (+/-) to clip based on the profile layer, in szUnits
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setTransform(bool bFlipX, bool bFlipY, double fRotation, bool bReverse, const char* szUnits, double fShiftX, double fShiftY, bool bContourize, double fDistortX, double fDistortY, bool bDeleteOutside, double dMarginX = 0.0, double dMarginY = 0.0);

    /*! cleanup layer(s) for anamorphic scale
    * the function should be used when all layers are loaded
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool cleanup();

    /**
    * Apply anamorphic scale to loaded layers
    *
    * @param[in] dScaleX - value for scale in the axes X in milimiters
    * @param[in] dScaleY - value for scale in the axes Y in milimeters
    * @param[in] bProportional - if true, the scale will be proportional
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool anamorphicScale(double fDistanceX, double fDistanceY, bool bProportional = true);

    /*! output all layers as gerber files (layer name will become the file name)
    *
    * @param[in] szResource path the the gerber CAD resource file (definition of the gerber dialect)
    * @param[in] szPath destination directory
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool outputGerber(const char* szResources, const char* szPath);

    /*! output all layers as gdsii files (layer name will become the file name)
    *
    * @param[in] szParams string defining conversion parameters (allowed values are DPF to generate DPF file for compare and ARCEXPAND= for arc expansion margin. Example used in Ucam is "DPF,PIECEWISE,ARCEXPAND=0.2mil")
    * @param[in] szPath destination directory
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool outputGdsii(const char* szParam, const char* szPath);

    /*! output all algnment layers (name starting with "r_" or "R_") as align files (layer name will become the file name)
    * If the layername does not start with "r_" or "R_", alignment points and plotstamps will be detected using the parameters
    * set by "setPlotstamp()" and "setFiducial()"
    *
    * @param[in] szPath destination directory
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool outputAlignment(const char* szPath);

    /*! set plotstamp by the aperture number
    *
    * @param[in] iNumber number of the aperture to look for
    * @param[in] szText defintion of the plotstamp as to be output to the file)
    * @param[in] fWidth defintion of the width of plotstamp), if 0, then take the size from the flash dimension
    * @param[in] fHeight defintion of the height of plotstamp), if 0, then take the size from the flash dimension
    * Note: if the width and height are setting as zero, then use width and height as the plotstamp, just replace it.
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setPlotstamp(int iNumber, const char* szText, double fWidth = 0.0, double fHeight = 0.0);

    /*! set plotstamp by the aperture name
    *
    * @param[in] szName name of the aperture to look for
    * @param[in] szText defintion of the plotstamp as to be output to the file)
    * @param[in] fWidth defintion of the width of plotstamp), if 0, then take the size from the flash dimension
    * @param[in] fHeight defintion of the height of plotstamp), if 0, then take the size from the flash dimension
    * Note: if the width and height are setting as zero, then use width and height as the plotstamp, just replace it.
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setPlotstamp(const char* szName, const char* szText, double fWidth = 0.0, double fHeight = 0.0);

    /*! set plotstamp by the aperture attribute
    *
    * @param[in] szAttribute name of the aperture attribute to look for
    * @param[in] szValue optional value of the attribute to search for (NULL if vale is to be ignored)
    * @param[in] szText defintion of the plotstamp as to be output to the file)
    * @param[in] fWidth defintion of the width of plotstamp), if 0, then take the size from the flash dimension
    * @param[in] fHeight defintion of the height of plotstamp), if 0, then take the size from the flash dimension
    * @param[in] bObjAttr if true check obj attributes, aperture attributes otherwise
    * Note: if the width and height are setting as zero, then use width and height as the plotstamp, just replace it.
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setPlotstamp(const char* szAttribute, const char* szValue, const char* szText, double fWidth = 0.0, double fHeight = 0.0, bool bObjAttr = false);

    /*! set single or matrix hole fiducial by the aperture number
    *
    * @param[in] iNumber number of the aperture to look for
    * @param[in] bMatrix if matrix needs to be searched
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setFiducial(int iNumber, bool bMatrix);

    /*! set single or matrix hole fiducial by the aperture name
    *
    * @param[in] szName name of the aperture to look for
    * @param[in] bMatrix if matrix needs to be searched
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setFiducial(const char* szName, bool bMatrix);

    /*! set single or matrix hole fiducial by the aperture name
    *
    * @param[in] szAttribute name of the aperture attribute to look for
    * @param[in] szValue optional value of the attribute to search for (NULL if vale is to be ignored)
    * @param[in] bMatrix if matrix needs to be searched
    * @param[in] bObjAttr if true check obj attributes, aperture attributes otherwise
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setFiducial(const char* szAttribute, const char* szValue, bool bMatrix, bool bObjAttr);

    /*! set plum hole fiducial by the aperture number
    *
    * @param[in] iCenter number of the center aperture to look for
    * @param[in] iNumber number of the aperture to look for
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setFiducialPlum(int iCenter, int iNumber);

    /*! set plum hole fiducial by the aperture name
    *
    * @param[in] szName name of the aperture to look for
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setFiducialPlum(const char* szCenter, const char* szName);

    /*! set plum hole fiducial by the aperture name
    *
    * @param[in] szAttribute name of the aperture attribute to look for
    * @param[in] szValue of the aperture attribute to look for (null for ignore)
    * @param[in] bWildcard if true, the szAttr may contain one '*' as a wildcard character
    *
    * @returns true in case of success, it fills last error when failed
    */
    bool setFiducialPlum(const char* szAttribute, const char* szValue = NULL, bool bWildcard = true);

    /*! image compare layer with the loaded layer
    *
    * @param[in] szPath dpf file to compare
    * @param[in] iPpi          The ppi to use for image compare.
    * @param[in] szErrorMargin The error margin to use (filter differences smaller than this).
    *
    * @returns amount of image differences
    */
    int imageCompare(const char* szPath, int iPpi, const char* szErrorMargin);

    //! job destructor
    virtual ~C_CFMEE();
    
    //! @return last error (NULL on no error)
    const char* lastError() const
    {
        return (szLastError_);
    }
    //!< type of the image bitmap - a png file is stored in memory buffer memory_ with size size_
    struct C_Bitmap {
      char* memory_; //!< memory buffer with PNG image of the layers(s)
      size_t size_; //!< buffer size

      C_Bitmap()
      : memory_(NULL)
      , size_(0)
      {
      }
    }; //struct C_Bitmap

    //! helper rectangle structure
    struct C_Rectangle {
      double fXmin_; //!< left boundary (in mm)
      double fXMax_; //!< right boundary (in mm)
      double fYmin_; //!< bottom boundary (in mm)
      double fYMax_; //!< top boundary (in mm)
    }; //struct C_Rectangle

    //! circle structure for identifying holes
    struct C_Circle {
      double fX_; //!< x coordinate of the center (in mm)
      double fY_; //!< y coordinate of the center (in mm)
      double fR_; //!< diameter of the center (in mm)
    }; //struct C_Circle

    //! helper struct to define the display area
    struct C_DisplayArea {
      C_Rectangle* outline_; //!< rectangle defining the outline of the display area
      double dpmm_; //!< resolution (in dots per mm);
    }; //struct C_DisplayArea

    //! @return enclosing box of loaded job, in mm
    C_Rectangle enclosing_box() const;

    /*! create bitmap of the loaded data for given rectangle
    * @param[in] displayArea rectangle to display and resolution density
    *
    * @returns newly allocated bitmap image, to be destroyed by the caller when not needed
    */
    C_Bitmap image(const C_DisplayArea& displayArea) const;

    /*! Add an alignment point. When the index is not passed, the alignment point will be added to the end.
    * When an aligmnent point with this index already exists, it will be replaced.
    * When an alignment point with this index does not exist yet, the alignment point will be added to the end.
    *
    * @param[in] displayArea the area in which the alignment point is selected
    * @param[in] x the x coordinate on the display area in pixels
    * @param[in] y the y coordinate on the display area in pixels
    * @param[in] index (optional) the index of the alignment point.
    *
    * @returns the real index of the alignment point or a negative value in case of error.
    */
    int setAlignmentPoint(const C_DisplayArea& displayArea, int x, int y, int index = 0);

    /*! Returns the C_Circle alignment point
    *
    * @param[in] index the index of the alignment point
    * @param[out] the C_Circle object
    *
    * @returns true of the alignment point was found, false if it doesn't exist.
    */
    bool getAlignmentPoint(int index, C_Circle& Circle) const;

    /*! This will load the outline of the panel. this information is necessary to output alingmentPoint information correctly
    *
    * @param[in] szOdbPath path the the ODB++ root directory
    * @param[in] szOdbJob Jobname
    * @param[in] szStep stepname
    *
    * @returns true in case of success
    */
    bool loadOutline(const char* szOdbPath, const char* szOdbJob, const char* szOdbStep);

    /*! output all selected algnmentment points
    * This method needs outline information! Outline layer should be loaded during odb import
    *
    * @param[in] szPath destination directory
    *
    * @returns true in case of success
    */
    bool outputAlignmentPoints(const char* szPath);

    /*! Delete objects by attribute
    * This method goes through the all objects in the job, and deletes those with a certain attribute
    *
    * @param[in] szAttributeName the attribute name
    * @param[in] szAttributeValue the attribute value, can be NULL
    */
    void deleteObjectsByAttribute(const char* szAttributeName, const char* szAttributeValue);

    /*! Get a list of loaded layer names
    *
    * @return NULL-terminated list of loaded layers (the structure is allocated locally inside C_Job instance, it is released when destroying it)
    */
    const char** getLoadedLayers();

    /*! Query a layer for a layer attribute
    *
    * @param[in] szLayername the name of the layer
    * @param[in] szAttrName the name of the attribute to search for
    *
    * @return the attribute value. If the attribute cannot be found, this method returns NULL. (The attributes are allocated locally inside C_Job instance, they are released when destroying it)
    */
    const char* queryLayerAttribute(const char* szLayername, const char* szAttrName);

    //! hash codes
    struct C_HashCodes {
      const char* szLayName_; //!< layer name
      size_t Hash_; //!< hash code of the layer image
    };
    C_HashCodes* list_job_hash();
  }; //class C_CFMEE

  /* example code
  using namespace ucam;
  // CFMEE imports ODB++ step/layer(s) perform contourize and outputs ger
  //input: ODB++ step/layer
  //output: Gerber or GDSII
  //
int main(int argc, char* argv[])
{
  std::cout << "CFMEE example conversion application\n";
  if (argc < 3) {
    std::cout << "Syntax: CFMEE [DB:ucam.db_path] [CAD:resource_cad_file] [OUT:]output_path ODB:<path>*<job>*<step>*<layer> {CONT}" << std::endl <<
      "         the DB: is the ucam.db settings" << std::endl <<
      "         the CAD: is the resource CAD settings" << std::endl <<
      "         the GEROUT:output path" << std::endl <<
      "         the GDSOUT:output path" << std::endl <<
      "         the ODB:<path>*<job>*<step>*<layer> layer/step to import" << std::endl;
  }
  else {
    const char* ucam_db = "";
    const char* cad_file = "";
    const char* license_map = "oem_cfmee";
    const char* odb = "";
    const char* out = "";
    bool gerout = false;
    bool gdsout = false;
    int from = 1;

    if (0 == STRNICMP(argv[from], "DB:", 3)) {
      ucam_db = argv[from] + 3;
      from++;
    }

    if (0 == STRNICMP(argv[from], "CAD:", 4)) {
      cad_file = argv[from] + 4;
      from++;
    }

    if (0 == STRNICMP(argv[from], "GEROUT:", 7)) {
      out = argv[from] + 4;
      gerout = true;
      from++;
    }

    if (0 == STRNICMP(argv[from], "GDSOUT:", 7)) {
      out = argv[from] + 4;
      gdsout = true;
      from++;
    }

    if (0 == STRNICMP(argv[from], "ODB:", 4)) {
      odb = argv[from] + 4;
      from++;
    }

    // odb param parse
    char buf[4096];

    strncpy_s(buf, odb, 4096);
    char* path = buf;
    char* jobname = strchr(path, '*');
    char* step = NULL;
    char* layer = NULL;
    if (jobname) {
      *jobname++ = '\0';
      step = strchr(jobname, '*');
      if (step) {
        *step++ = '\0';
        layer = strchr(step, '*');
        if (layer) {
          *layer++ = '\0';
          std::cout << "ODB++ import path=[" << path << "] job=[" << jobname << "] step=[" << step << "] layer=[" << layer << "]" << std::endl;
        }
        else {
          std::cerr << "ODB++ layer not defined\n" << std::endl;
        }
      }
      else {
        std::cerr << "ODB++ step not defined\n" << std::endl;
      }
    }

    try {
      ucam::Initialize(ucam_db, "oem_cfmee");
      ucam::C_CFMEE job(jobname);

      // print steps and layers
      printf("Steps\n");
      const char** steps = job.getODBStepList(odb_path, odb_job);
      for (const char** p = steps; p && *p; p++) {
        printf("  %s\n", *p);
      }
      printf("\n");

      printf("Layers\n");
      const char** layers = job.getODBLayerList(odb_path, odb_job);
      for (const char** p = layers; p && *p; p++) {
        printf("  %s\n", *p);
      }
      printf("\n");

      if (jobname && step && layer) {
        job.importODBStep(odb_path, odb_jobname, step, layer);
      }
      else {
        printf("ODB++ job/step/layer not properly defined\n");
      }

      if (gdsout) {
        job.outputGdsii("DPF,ARCEXPANDMARGIN=1mil,PIECEWISE", out);
      }
      else if (gerout) {
        job.outputGerber(cad_file, out);
      }
      else {
        printf("No ouput specified\n");
      }
    }
    catch (ucam::C_ConvException e) {
      printf("Conversion error %s\n", e.getErrorDescription());
    }
  }
  return 0;
}
*/
} //namespace ucam
