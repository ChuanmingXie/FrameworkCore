#pragma once

// ������ DLL �ļ���

#include "UcamHelper.h"
#include <iostream>



using namespace std;

/* UcamHelper ���캯�� */
UcamApi::UcamHelper::UcamHelper(String^ ucamdb)
{
	str_ucamdb = ucamdb;
	m_logFile = NULL;
}

/* UcamHelper �������� */
UcamApi::UcamHelper::~UcamHelper()
{
	if (this) {
		delete this;
	}
}

/* ��ʼ��תͼ����
* @param[in] ucam.db λ��
* @param[in] licenseId���
* @param[in] ��־�ļ�·��?
*/
void UcamApi::UcamHelper::Initialize(String^ ucamdb, String^ licenseId, String^ logFile) {
	if (!m_initialized) {
		str_ucamdb = ucamdb;
		str_licenseID = licenseId;
		try {
			if (!String::IsNullOrEmpty(logFile)) {
				m_logFile = fopen(String2CharPointer(logFile), "wt+");
				ucam::Initialize(String2CharPointer(ucamdb), String2CharPointer(licenseId), m_logFile);
			}
			else {
				m_logFile = NULL;
				ucam::Initialize(String2CharPointer(ucamdb), String2CharPointer(licenseId));
			}
			m_initialized = true;
		}
		catch (C_ConvException e) {
			HandleConveterException(e);
		}
		catch (...) {
			HandlesException("Initialize Error");
		}
		finally {
			fclose(m_logFile);
		}
	}
}

/* ����Job����
* @param[in] Job����
*/
void UcamApi::UcamHelper::CreateNewJob(String^ strJobName) {
	ReleaseCurrentJob();
	try {
		str_jobName = strJobName;
		m_job = new C_CFMEE(String2CharPointer(strJobName));
		m_logFile = fopen(String2CharPointer("Job " + strJobName + "Created!"), "wt+");
	}
	catch (...) {
		HandlesException("CreateNewJob Error");
	}
	finally {
		fclose(m_logFile);
	}
}

/* ��ȡODB���Ϲ������Step��Ϣ
* @param[in] tgz����·��
*
* @return �����б�
*/
List<String^>^ UcamApi::UcamHelper::GetOdbSteps(String^ strFile) {
	List<String^>^ list = gcnew List<String^>();
	if (m_job != NULL) {
		try
		{
			const char** steps = m_job->getODBStepList(String2CharPointer(strFile), String2CharPointer(str_jobName));
			if (steps != NULL) {
				while (steps != NULL && *steps != NULL)
					list->Add(CharPointer2String(*steps++)->ToLower());
			}
		}
		catch (C_ConvException e) {
			HandleConveterException(e);
		}
		catch (...) {
			HandlesException("GetOdbSteps Error");
		}
	}
	return list;
}

/* ��ȡ�Ѿ����صĲ�����
* @param[in] tgz����·��
*
* @return ����б�
*/
List<String^>^ UcamApi::UcamHelper::GetOdbLayers(String^ strFile) {
	List<String^>^ list = gcnew List<String^>();
	if (m_job == NULL) return list;
	try {
		const char** layers = m_job->getODBLayerList(String2CharPointer(strFile), String2CharPointer(str_jobName));
		if (layers != NULL) {
			while (layers != NULL && *layers != NULL)
			{
				list->Add(CharPointer2String(*layers++)->ToLower());
			}
		}
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("GetOdbLayers Error");
	}
	return list;
}

/* ��ȡ�Ѿ����صĲ�����
*/
List<String^>^ UcamApi::UcamHelper::GetLoadedLayers() {
	List<String^>^ list = gcnew List<String^>();
	if (m_job == NULL) return list;
	try {
		const char** layers = m_job->getLoadedLayers();
		if (layers != NULL) {
			while (layers != NULL && *layers != NULL)
			{
				list->Add(CharPointer2String(*layers++)->ToLower());
			}
		}
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("GetLoadedLayers Error");
	}
	return list;
}

/* ����ODB���ķ�ʽ
* @param[in] �ļ�·��
* @param[in] ������������
* @param[in] �������
*
* @return ���سɹ��ı�־
*/
bool UcamApi::UcamHelper::ImportOdbLayer(String^ filePath, String^ stepName, String^ layerName) {
	if (m_job == NULL) CreateNewJob(str_jobName);
	if (m_job == NULL) return false;
	try {
		return m_job->importODBStep(String2CharPointer(filePath)
			, String2CharPointer(str_jobName)
			, String2CharPointer(stepName)
			, String2CharPointer(layerName));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("ImportOdbLayer Error");
	}
	return false;
}

/* ����Gerber���ķ�ʽ
* @param[in] �ļ�·��
*
* @return ���سɹ��ı�־
*/
bool UcamApi::UcamHelper::ImportGerber(String^ filePath) {
	if (m_job == NULL) CreateNewJob(str_jobName);
	if (m_job == NULL) return false;
	try {
		return m_job->import274X(String2CharPointer(filePath));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("ImportGerber Error");
	}
	return false;
}

/* ����DPF���ķ�ʽ
* @param[in] �ļ�·��
*
* @return ���سɹ��ı�־
*/
bool UcamApi::UcamHelper::LoadDPF(String^ filePath) {
	if (m_job == NULL) CreateNewJob(str_jobName);
	if (m_job == NULL) return false;
	try {
		return m_job->loadDpf(String2CharPointer(filePath));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("LoadDPF Error");
	}
	return false;
}

/* �������в��
*/
bool UcamApi::UcamHelper::AllLayersLoaded() {
	if (m_job == NULL) return false;
	try {
		return m_job->cleanup();
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("AllLayersLoaded Error");
	}
	return false;
}

/* תͼ�����趨 Ĭ�ϲ�����(�����ܱ�0),����1;1
* @param[in] flipX ��X������(���ұ�,������)
* @param[in] flipY ��Y������(���±�,������)
* @param[in] rotation   ��ת�Ƕ�(��ʱ��)
* @param[in] polarity   ����
* @param[in] units      ͼ�γ��ȵ�λ
* @param[in] shiftX     X��������
* @param[in] shiftY     Y��������
* @param[in] contourize ������
*/
bool UcamApi::UcamHelper::SetTransform(bool flipX, bool flipY, int rotation
	, bool polarity, int units, double shiftX, double shiftY, bool contourize) {
	return SetTransform(flipX, flipY, rotation, polarity, units, shiftX, shiftY, contourize, true, 0.0, 0.0);
}

/* תͼ�����趨 Ĭ��������,����1;1
* @param[in] flipX ��X������(���ұ�,������)
* @param[in] flipY ��Y������(���±�,������)
* @param[in] rotation   ��ת�Ƕ�(��ʱ��)
* @param[in] polarity   ����
* @param[in] units      ͼ�γ��ȵ�λ
* @param[in] shiftX     X��������
* @param[in] shiftY     Y��������
* @param[in] delOutLine �Ƿ����
* @param[in] delMargin  �ܱ߲���ֵ
*
* @return �������óɹ���־
*/
bool UcamApi::UcamHelper::SetTransform(bool flipX, bool flipY, int rotation
	, bool polarity, int units, double shiftX, double shiftY, bool delOutLine, double delMargin) {
	return SetTransform(flipX, flipY, rotation, polarity, units, shiftX, shiftY, true, delOutLine, delMargin, delMargin);
}

/* תͼ�����趨 Ĭ������1;1
* @param[in] flipX ��X������(���ұ�,������)
* @param[in] flipY ��Y������(���±�,������)
* @param[in] rotation   ��ת�Ƕ�(��ʱ��)
* @param[in] polarity   ����
* @param[in] units      ͼ�γ��ȵ�λ
* @param[in] shiftX     X��������
* @param[in] shiftY     Y��������
* @param[in] contourize ������
* @param[in] delOutLine �Ƿ����
* @param[in] delMarginX �������Ҳ���ֵ
* @param[in] delMarginY �������²���ֵ
*
* @return �������óɹ���־
*/
bool UcamApi::UcamHelper::SetTransform(bool flipX, bool flipY, int rotation
	, bool polarity, int units, double shiftX, double shiftY, bool contourize
	, bool delOutLine, double delMarginX, double delMarignY) {
	return SetTransform(flipX, flipY, rotation, polarity, units, shiftX, shiftY, contourize
		, 1.0, 1.0, delOutLine, delMarginX, delMarignY);
}

/* תͼ�����趨
* @param[in] flipX ��X������(���ұ�,������)
* @param[in] flipY ��Y������(���±�,������)
* @param[in] rotation   ��ת�Ƕ�(��ʱ��)
* @param[in] polarity   ����
* @param[in] units      ͼ�γ��ȵ�λ
* @param[in] shiftX     X��������
* @param[in] shiftY     Y��������
* @param[in] contourize ������
* @param[in] scaleX     ˮƽ��������
* @param[in] scaleY		��ֱ��������
* @param[in] delOutLine �Ƿ����
* @param[in] delMarginX �������Ҳ���ֵ
* @param[in] delMarginY �������²���ֵ
*
* @return �������óɹ���־
*/
bool UcamApi::UcamHelper::SetTransform(bool flipX, bool flipY, int rotation
	, bool polarity, int units, double shiftX, double shiftY, bool contourize
	, double scaleX, double scaleY, bool delOutLine, double delMarginX, double delMarignY) {
	if (m_job == NULL) return false;
	char* useUnit = "";
	switch (units)
	{
	case 0:useUnit = "mm"; break;
	case 2:useUnit = "mil"; break;
	case 1:useUnit = "inch"; break;
	default:
		break;
	}
	try {
		return m_job->setTransform(flipX, flipY, rotation, polarity, useUnit, shiftX, shiftY
			, contourize, scaleX, scaleY, delOutLine, delMarginX, delMarignY);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetTransform Error");
	}
	return false;
}

/* �߿�����ʽ
* @param[in] ˮƽ�����߿���
* @param[in] ��ֱ�����߿���
*
* @return �����ɹ���־
*/
bool UcamApi::UcamHelper::ShapeCompensation(double distanceX, double distanceY) {
	return ShapeCompensation(distanceX, distanceY, true);
}

/* �߿�����ʽ
* @param[in] ˮƽ�����߿���
* @param[in] ��ֱ�����߿���
* @param[in] �Ƿ񰴱�������?
*
* @return �����ɹ���־
*/
bool UcamApi::UcamHelper::ShapeCompensation(double distanceX, double distanceY, bool proportion) {
	if (m_job == NULL) return false;
	try {
		return m_job->cleanup() && m_job->anamorphicScale(distanceX, distanceY, proportion);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("ShapeCompensation Error");
	}
	return false;
}

/* ����Profile��ķ���
* @param[in] profile·��
*/
void UcamApi::UcamHelper::SetOdbProfile(String^ profilePath) {
	if (m_job == NULL) return;
	try {
		m_job->setODBProfile(String2CharPointer(profilePath));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetOdbProfile Error");
	}
}

/* ���ر߿�
* @param[in] odb��Ŀ¼
* @param[in] job ����(·��)
* @param[in] step ��������(·��)
*
* @return ���سɹ���־
*/
bool UcamApi::UcamHelper::LoadOutline(String^ odbRootPath, String^ jobName, String^ stepName) {
	if (m_job == NULL) return false;
	try {
		return m_job->loadOutline(String2CharPointer(odbRootPath), String2CharPointer(jobName), String2CharPointer(stepName));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("LoadOutline Error");
	}
	return false;
}

/* ͨ��aperture����ͨ������
* @param[in] D�� (aperture ����ֵ)
* @param[in] �Ƿ�Ϊ����?
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetFiducial(Int32 dCode, bool bMatrix) {
	if (m_job == NULL) return false;
	try {
		return m_job->setFiducial(dCode, bMatrix);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetFiducial Error");
	}
	return false;
}

/* ͨ��aperture����ͨ������
* @param[in] Symbol����(aperture ������)
* @param[in] �Ƿ�Ϊ����?
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetFiducial(String^ symbolName, bool bMatrix) {
	if (m_job == NULL) return false;
	try {
		return m_job->setFiducial(String2CharPointer(symbolName), bMatrix);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetFiducial Error");
	}
	return false;
}

/* ͨ����������ͨ������
* @param[in] ��������
* @param[in] ����ֵ
* @param[in] �Ƿ�Ϊ����?
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetFiducial(String^ attrName, String^ attrValue, bool bMatrix) {
	return SetFiducial(attrName, attrValue, bMatrix, true);
}

/* ͨ����������ͨ������
* @param[in] ��������
* @param[in] ����ֵ
* @param[in] �Ƿ�Ϊ����?
* @param[in] �Ƿ�Ϊattribute����aperture ����?
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetFiducial(String^ attrName, String^ attrValue, bool bMatrix, bool bObjAttr) {
	if (m_job == NULL) return false;
	try {
		return m_job->setFiducial(String2CharPointer(attrName), String2CharPointer(attrValue), bMatrix, bObjAttr);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetFiducial Error");
	}
	return false;
}

/* ͨ��aperture����÷��������
* @param[in] aperture Center
* @param[in] D�� (aperture Number)
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetFiducialPlum(Int32 iCenter, Int32 dCode) {
	if (m_job == NULL) return false;
	try {
		return m_job->setFiducialPlum(iCenter, dCode);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetFiducialPlum Error");
	}
	return false;
}

/* ͨ���������� ����÷������Ϣ
* @param[in] ������
* @param[in] ����ֵ
* @param[in] WildCard Ϊtrue
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetFiducialPlum(String^ attrName, String^ attrValue) {
	return SetFiducialPlum(attrName, attrValue, true);
}

/* ͨ���������� ����÷������Ϣ
* @param[in] ������
* @param[in] ����ֵ
* @param[in] �Ƿ�ΪWildCard ?
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetFiducialPlum(String^ attrName, String^ attrValue, bool bWildcard) {
	if (m_job == NULL) return false;
	try {
		return m_job->setFiducialPlum(String2CharPointer(attrName), String2CharPointer(attrValue), bWildcard);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetFiducialPlum Error");
	}
	return false;
}

/* ����D��(aperture number)����
* @param[in] D��
* @param[in] �ַ���־
* @param[in] �ַ���
* @param[in] �ַ���
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetPlotStamp(Int32 dCode, String^ stampSign, double width, double height) {
	if (m_job == NULL) return false;
	try {
		return m_job->setPlotstamp(dCode, String2CharPointer(stampSign), width, height);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetPlotStamp Error");
	}
	return false;
}

/* ����D��(aperture number)����
* @param[in] symbol Name
* @param[in] �ַ���־
* @param[in] �ַ���
* @param[in] �ַ���
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetPlotStamp(String^ symbolName, String^ stampSign, double width, double height) {
	if (m_job == NULL) return false;
	try {
		return m_job->setPlotstamp(String2CharPointer(symbolName)
			, String2CharPointer(stampSign), width, height);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetPlotStamp Error");
	}
	return false;
}

/* ����D��(aperture number)����
* @param[in] ������
* @param[in] ����ֵ
* @param[in] �ַ���־
* @param[in] �ַ���
* @param[in] �ַ���
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetPlotStamp(String^ attrName, String^ attrValue, String^ stampSign, double width, double height) {
	if (m_job == NULL) return false;
	try {
		return m_job->setPlotstamp(String2CharPointer(attrName)
			, String2CharPointer(attrValue)
			, String2CharPointer(stampSign), width, height);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetPlotStamp Error");
	}
	return false;
}

/* ����D��(aperture number)����
* @param[in] ������
* @param[in] ����ֵ
* @param[in] �ַ���־
* @param[in] �ַ���
* @param[in] �ַ���
* @param[in] �Ƿ�Ϊ�������Ի���aperture����?
*
* @return ���óɹ��ķ���ֵ
*/
bool UcamApi::UcamHelper::SetPlotStamp(String^ attrName, String^ attrValue, String^ stampSign, double width, double height, bool bObjAttr) {
	if (m_job == NULL) return false;
	try {
		return m_job->setPlotstamp(String2CharPointer(attrName)
			, String2CharPointer(attrValue)
			, String2CharPointer(stampSign), width, height, bObjAttr);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("SetPlotStamp Error");
	}
	return false;
}

/* ��������ɾ��ͼ��(Ŀǰ�����������)
* @param[in] ������
* @param[in] ����ֵ
*/
void UcamApi::UcamHelper::DeletePatternByAttribute(String^ attrName, String^ attrValue) {
	if (m_job == NULL) return;
	try {
		m_job->deleteObjectsByAttribute(String2CharPointer(attrName), String2CharPointer(attrValue));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("DeletePatternByAttribute Error");
	}
}

/* �����Ϣ���ȵ�λ,��λ,�ַ�,ͼ�δ�С��Ϣ�ķ���
* @param[in] ͼ��λ��
*
* @return ����ɹ���־
*/
bool UcamApi::UcamHelper::OutputAlignment(String^ filePath) {
	if (m_job == NULL) return false;
	try {
		return m_job->outputAlignment(String2CharPointer(filePath));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("OutputAlignment Error");
	}
	return false;
}

/* ֻ�����λ���λ�İ취?
* @param[in] ͼ��λ��
*
* @return ����ɹ���־
*/
bool UcamApi::UcamHelper::OutputAlignmentPoints(String^ filePath) {
	if (m_job == NULL) return false;
	try {
		return m_job->outputAlignmentPoints(String2CharPointer(filePath));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("OutputAlignmentPoints Error");
	}
	return false;
}

/* ���ɲ��Hsah��
*
* @param[in] hashָ��
*
* @return ���ɳɹ���־
*/
bool UcamApi::UcamHelper::GenerateHashCode(C_HashCode^ hashPointer) {
	if (m_job == NULL) return false;
	try {
		hashPointer->szLayerName = CharPointer2String(m_job->list_job_hash()->szLayName_);
		hashPointer->HashInfo = m_job->list_job_hash()->Hash_;
		return true;
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("GenerateHashCode Error");
	}
	return false;
}

/* ���GDS��תͼ����
*
* @param[in] job�ļ�·��
* @param[in] arcMargin
*
* @return ת���ɹ���־
*/
bool UcamApi::UcamHelper::OutputGDSII(String^ filePath) {
	return OutputGDSII(filePath, 5);
}

/* ���GDS��תͼ����
*
* @param[in] job�ļ�·��
* @param[in] arcMargin
*
* @return ת���ɹ���־
*/
bool UcamApi::UcamHelper::OutputGDSII(String^ filePath, double arcMargin) {
	if (m_job == NULL) return false;
	try {
		double marginMil = arcMargin / 25.4;
		//String^ strParam = String::Format("DPF,PIECEWISE,ARCEXPANDMARGIN={0:F3}mil", mil);
		//String^ strParam = "DPF,PIECEWISE,ARCEXPAND=0.001mm";
		//String^ strParam = "DPF,ARCEXPAND=0.001mm";
		String^ strParam = "DPF,ARCEXPAND=0.001mm,PIECEWISE";
		return m_job->outputGdsii(String2CharPointer(strParam), String2CharPointer(filePath));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("OutputGDSII Error");
	}
	return false;
}

/* ���Gerber��תͼ����
*
* @param[in] job�ļ�·��
*
* @return ת���ɹ���־
*/
bool UcamApi::UcamHelper::OutputGerber(String^ filePath) {
	if (m_job == NULL) return false;
	try {
		return m_job->outputGerber("", String2CharPointer(filePath));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("OutputGerber Error");
	}
	return false;
}

/* ͼ�αȶ�
*
* @param[in] job�ļ�·��
* @param[in] ppi
* @param[in] �ȶԲ���
*
* @return �ȶԴ������
*/
int UcamApi::UcamHelper::ImageCompare(String^ filePath, int ppi, String^ strParam) {
	if (m_job == NULL) return -2;
	try {
		return m_job->imageCompare(String2CharPointer(filePath), ppi, String2CharPointer(strParam));
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("ImageCompare Error");
	}
	return -1;
}

/* �ͷ�תͼ����
*/
void UcamApi::UcamHelper::Release() {
	ReleaseCurrentJob();
	m_initialized = false;
}

/* �ͷŵ�ǰתͼ����
*/
void UcamApi::UcamHelper::ReleaseCurrentJob() {
	if (m_logFile != NULL)
		fclose(m_logFile);
	if (m_job != NULL)
	{
		m_job = NULL;
		delete m_job;
	}
}

/* ��ȡ��ǰִ�й��̵ķ���:��ʼ��,����,����,�쳣
*
* @return E_PROGRESS_STATUS �ṹ���е�����
*/
int UcamApi::UcamHelper::GetCurrentProgress() {
	if (m_job == NULL) return 0;
	try {
		size_t total = 0;
		size_t done = 0;
		E_PROGRESS_STATUS ps = ucam::Progress(done, total);
		switch (ps)
		{
		case ucam::PROGRESS_IDLE:return 0;
			break;
		case ucam::PROGRESS_IMPORT:return (int)(100.0 * done / total);
			break;
		case ucam::PROGRESS_EXPORT:return (int)(100.0 * done / total);
			break;
		case ucam::PROGRESS_ERROR:return -100;
			break;
		default:
			break;
		}
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("GetCurrentProgress Error");
	}
	return false;
}

/* ��ȡ������ֵ(��attrlist�л�ȡ�����Ϣ)
*
* @param[in] ����
* @param[in] ������
*
* @return ����ֵ����ֵ
*/
String^ UcamApi::UcamHelper::QueryLayerAttribute(String^ layerName, String^ attributeName) {
	if (m_job == NULL)
		return String::Empty;
	try
	{
		const char* data = m_job->queryLayerAttribute(String2CharPointer(layerName), String2CharPointer(attributeName));
		if (data)
			return CharPointer2String(data);
	}
	catch (C_ConvException e) {
		HandleConveterException(e);
	}
	catch (...) {
		HandlesException("QueryLayerAttribute Error");
	}
	return String::Empty;
}

/* ��ȡ�쳣
*
* @param[in] e�쳣��Ϣ
*
* @return ����ֵΪ��
*/
void UcamApi::UcamHelper::HandleConveterException(C_ConvException& e) {
	//String^ strMsg = CharPointer2String(m_job ? m_job->lastError() : e.getErrorDescription());
	//System::Windows::MessageBox::Show(strMsg, "Ucam-Error"
	//	, System::Windows::MessageBoxButton::OK
	//	, System::Windows::MessageBoxImage::Error);
	//throw gcnew System::Exception(strMsg);
}

/*  ��ȡ�쳣
*
* @param[in] e�쳣��Ϣ
*
* @return ����ֵΪ��
*/
void UcamApi::UcamHelper::HandlesException(const char* e) {
	//String^ strMsg = CharPointer2String(e);
	//System::Windows::MessageBox::Show(strMsg, "Ucam-Error"
	//	, System::Windows::MessageBoxButton::OK
	//	, System::Windows::MessageBoxImage::Error);
	//throw gcnew System::Exception(strMsg);
}

/* ��ȡ�����ʾ���쳣
*
* @param[in] ����Ϊ��
*
* @return ����ֵ�쳣��Ϣ
*/
String^ UcamApi::UcamHelper::GetLastError() {
	if (!m_job) return String::Empty;
	return CharPointer2String(m_job->lastError());
}

/* ��������
*
* @param[in] ����Ϊ��
*
* @return ����ֵΪtrue��ʾ�������,false��ʾ����ʧ��
*/
bool UcamApi::UcamHelper::Cleanup() {
	if (m_job == NULL)
		return false;
	try
	{
		return m_job->cleanup();
	}
	catch (C_ConvException ex)
	{
		HandleConveterException(ex);
		return false;
	}
}

/* �ַ���ת��
*
* @param[in] string�����ַ���
*
* @return C++�����ַ���(char*)
*/
char* UcamApi::UcamHelper::String2CharPointer(String^ str) {
	if (String::IsNullOrEmpty(str)) return "";
	return (char*)(System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(str).ToPointer());
}

/* �ַ���ת��
*
* @param[in] C++�����ַ���(char*)
*
* @return string�����ַ���
*/
String^ UcamApi::UcamHelper::CharPointer2String(const char* pChar) {
	return System::Runtime::InteropServices::Marshal::PtrToStringAnsi(IntPtr((void*)pChar));
}

