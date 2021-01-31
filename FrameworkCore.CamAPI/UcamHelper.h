// UcamHelper.h

#pragma once

#include "cfmee.h"
#include<exception>
using namespace ucam;
using namespace System;
using namespace System::Collections;
using namespace System::Collections::Generic;


namespace UcamApi {

	/* 定义承载Hash码数据结构体 */
	public ref struct C_HashCode
	{
		String^ szLayerName;
		size_t HashInfo;
	};

	public ref class UcamHelper
	{
	private:
		String^ str_ucamdb;
		String^ str_jobName;
		String^ str_licenseID;
		C_CFMEE* m_job;
		FILE* m_logFile;
		bool m_initialized;

	public:
		UcamHelper(String^ ucamdb);
		~UcamHelper();

		property String^ UcamDBPath
		{
			String^ get() {
				return str_ucamdb;
			}
			void set(String^ str) {
				str_ucamdb = str;
			}
		}

		property String^ JobName {
			String^ get() {
				return str_jobName;
			}
			void set(String^ str) {
				str_jobName = str;
			}
		}

		property String^ LicensedID {
			String^ get() {
				return str_licenseID;
			}
			void set(String^ str) {
				str_licenseID = str;
			}
		}

	public:
		/* 初始化转图对象 */
		void Initialize(String^ ucamdb, String^ licenseId, String^ logFile);

		/* 创建Job对象 */
		void CreateNewJob(String^ strJobName);

		/* 获取ODB资料工艺类别Step对象 */
		List<String^>^ GetOdbSteps(String^ strFile);

		/* 获取ODB资料层别对象 */
		List<String^>^ GetOdbLayers(String^ strFile);

		/* 获取已经加载的层别对象 */
		List<String^>^ GetLoadedLayers();

		/* 4 种加载层别的方式 */
		bool ImportOdbLayer(String^ filePath, String^ stepName, String^ layerName);
		bool ImportGerber(String^ filePath);
		bool LoadDPF(String^ filePath);
		bool AllLayersLoaded();

		/* 4 种参数设定方式(含历史) */
		bool SetTransform(bool flipX, bool flipY, int rotation, bool polarity, int units, double shiftX, double shiftY, bool contourize);
		bool SetTransform(bool flipX, bool flipY, int rotation, bool polarity, int units, double shiftX, double shiftY, bool delOutLine, double delMargin);
		bool SetTransform(bool flipX, bool flipY, int rotation, bool polarity, int units, double shiftX, double shiftY, bool contourize, bool delOutLine, double delMarginX, double delMarignY);
		bool SetTransform(bool flipX, bool flipY, int rotation, bool polarity, int units, double shiftX, double shiftY, bool contourize, double scaleX, double scaleY, bool delOutLine, double delMarginX, double delMarignY);

		/* 2 种线宽补偿方式*/
		bool ShapeCompensation(double distanceX, double distanceY);
		bool ShapeCompensation(double distanceX, double distanceY, bool proportion);

		/* 1 种设置Profile层的方法 */
		void SetOdbProfile(String^ profilePath);

		/* 加载边框 */
		bool LoadOutline(String^ odbRootPath, String^ jobName, String^ stepName);

		/* 4 种设置单孔属性的方法 */
		bool SetFiducial(Int32 dCode, bool bMatrix);
		bool SetFiducial(String^ symbolName, bool bMatrix);
		bool SetFiducial(String^ attrName, String^ attrValue, bool bMatrix);
		bool SetFiducial(String^ attrName, String^ attrValue, bool bMatrix, bool bObjAttr);

		/* 3 种设置梅花孔属性的方法(暂未使用) */
		bool SetFiducialPlum(Int32 iCenter, Int32 dCode);
		bool SetFiducialPlum(String^ attrName, String^ attrValue);
		bool SetFiducialPlum(String^ attrName, String^ attrValue, bool bWildcard);

		/* 4 种设置字符属性的方法 */
		bool SetPlotStamp(Int32 dCode, String^ stampSign, double width, double height);
		bool SetPlotStamp(String^ symbolName, String^ stampSign, double width, double height);
		bool SetPlotStamp(String^ attrName, String^ attrValue, String^ stampSign, double width, double height);
		bool SetPlotStamp(String^ attrName, String^ attrValue, String^ stampSign, double width, double height, bool bObjAttr);

		/* 按属性删除图案(一般为数码管) */
		void DeletePatternByAttribute(String^ attrName, String^ attrValue);

		/* 1 种输出unit,孔位,字符,大小信息的方法 */
		bool OutputAlignment(String^ filePath);
		/* 1 种输出对位层孔位的方法(仅输出) */
		bool OutputAlignmentPoints(String^ filePath);

		/* 获取层别哈希码 */
		bool GenerateHashCode(C_HashCode^ hashPointer);

		/* 2 种输出GDS的方式 */
		bool OutputGDSII(String^ filePath);
		bool OutputGDSII(String^ filePath, double arcMargin);
		/* 1 种输入Gerber的方式 */
		bool OutputGerber(String^ filePath);

		/* 执行图形比对 */
		int ImageCompare(String^ filePath, int ppi, String^ strParam);

		/* 释放转图对象 */
		void Release();
		void ReleaseCurrentJob();

		/* 获取当前执行过程的方法:初始化,导入,导出,异常 */
		int GetCurrentProgress();
		/* 查询层别属性(方法无效) */
		String^ QueryLayerAttribute(String^ layerName, String^ attributeName);

		/* 异常捕获处理 */
		void HandleConveterException(C_ConvException& e);
		void HandlesException(const char* e);
		/* 获取最后错误提示 */
		String^ GetLastError();
		/* 清空 */
		bool Cleanup();

	private:
		static char* String2CharPointer(String^ str);
		static String^ CharPointer2String(const char* pchar);
	};
}
