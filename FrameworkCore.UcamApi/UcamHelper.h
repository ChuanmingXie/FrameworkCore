// UcamHelper.h

#pragma once

#include "cfmee.h"
#include<exception>
using namespace ucam;
using namespace System;
using namespace System::Collections;
using namespace System::Collections::Generic;


namespace UcamApi {

	/* �������Hash�����ݽṹ�� */
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
		/* ��ʼ��תͼ���� */
		void Initialize(String^ ucamdb, String^ licenseId, String^ logFile);

		/* ����Job���� */
		void CreateNewJob(String^ strJobName);

		/* ��ȡODB���Ϲ������Step���� */
		List<String^>^ GetOdbSteps(String^ strFile);

		/* ��ȡODB���ϲ����� */
		List<String^>^ GetOdbLayers(String^ strFile);

		/* ��ȡ�Ѿ����صĲ����� */
		List<String^>^ GetLoadedLayers();

		/* 4 �ּ��ز��ķ�ʽ */
		bool ImportOdbLayer(String^ filePath, String^ stepName, String^ layerName);
		bool ImportGerber(String^ filePath);
		bool LoadDPF(String^ filePath);
		bool AllLayersLoaded();

		/* 4 �ֲ����趨��ʽ(����ʷ) */
		bool SetTransform(bool flipX, bool flipY, int rotation, bool polarity, int units, double shiftX, double shiftY, bool contourize);
		bool SetTransform(bool flipX, bool flipY, int rotation, bool polarity, int units, double shiftX, double shiftY, bool delOutLine, double delMargin);
		bool SetTransform(bool flipX, bool flipY, int rotation, bool polarity, int units, double shiftX, double shiftY, bool contourize, bool delOutLine, double delMarginX, double delMarignY);
		bool SetTransform(bool flipX, bool flipY, int rotation, bool polarity, int units, double shiftX, double shiftY, bool contourize, double scaleX, double scaleY, bool delOutLine, double delMarginX, double delMarignY);

		/* 2 ���߿�����ʽ*/
		bool ShapeCompensation(double distanceX, double distanceY);
		bool ShapeCompensation(double distanceX, double distanceY, bool proportion);

		/* 1 ������Profile��ķ��� */
		void SetOdbProfile(String^ profilePath);

		/* ���ر߿� */
		bool LoadOutline(String^ odbRootPath, String^ jobName, String^ stepName);

		/* 4 �����õ������Եķ��� */
		bool SetFiducial(Int32 dCode, bool bMatrix);
		bool SetFiducial(String^ symbolName, bool bMatrix);
		bool SetFiducial(String^ attrName, String^ attrValue, bool bMatrix);
		bool SetFiducial(String^ attrName, String^ attrValue, bool bMatrix, bool bObjAttr);

		/* 3 ������÷�������Եķ���(��δʹ��) */
		bool SetFiducialPlum(Int32 iCenter, Int32 dCode);
		bool SetFiducialPlum(String^ attrName, String^ attrValue);
		bool SetFiducialPlum(String^ attrName, String^ attrValue, bool bWildcard);

		/* 4 �������ַ����Եķ��� */
		bool SetPlotStamp(Int32 dCode, String^ stampSign, double width, double height);
		bool SetPlotStamp(String^ symbolName, String^ stampSign, double width, double height);
		bool SetPlotStamp(String^ attrName, String^ attrValue, String^ stampSign, double width, double height);
		bool SetPlotStamp(String^ attrName, String^ attrValue, String^ stampSign, double width, double height, bool bObjAttr);

		/* ������ɾ��ͼ��(һ��Ϊ�����) */
		void DeletePatternByAttribute(String^ attrName, String^ attrValue);

		/* 1 �����unit,��λ,�ַ�,��С��Ϣ�ķ��� */
		bool OutputAlignment(String^ filePath);
		/* 1 �������λ���λ�ķ���(�����) */
		bool OutputAlignmentPoints(String^ filePath);

		/* ��ȡ����ϣ�� */
		bool GenerateHashCode(C_HashCode^ hashPointer);

		/* 2 �����GDS�ķ�ʽ */
		bool OutputGDSII(String^ filePath);
		bool OutputGDSII(String^ filePath, double arcMargin);
		/* 1 ������Gerber�ķ�ʽ */
		bool OutputGerber(String^ filePath);

		/* ִ��ͼ�αȶ� */
		int ImageCompare(String^ filePath, int ppi, String^ strParam);

		/* �ͷ�תͼ���� */
		void Release();
		void ReleaseCurrentJob();

		/* ��ȡ��ǰִ�й��̵ķ���:��ʼ��,����,����,�쳣 */
		int GetCurrentProgress();
		/* ��ѯ�������(������Ч) */
		String^ QueryLayerAttribute(String^ layerName, String^ attributeName);

		/* �쳣������ */
		void HandleConveterException(C_ConvException& e);
		void HandlesException(const char* e);
		/* ��ȡ��������ʾ */
		String^ GetLastError();
		/* ��� */
		bool Cleanup();

	private:
		static char* String2CharPointer(String^ str);
		static String^ CharPointer2String(const char* pchar);
	};
}
