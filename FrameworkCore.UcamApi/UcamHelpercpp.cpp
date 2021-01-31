#pragma once

// 这是主 DLL 文件。

#include "UcamHelper.h"
#include <iostream>



using namespace std;

/* UcamHelper 构造函数 */
UcamApi::UcamHelper::UcamHelper(String^ ucamdb)
{
	str_ucamdb = ucamdb;
	m_logFile = NULL;
}

/* UcamHelper 析构函数 */
UcamApi::UcamHelper::~UcamHelper()
{
	if (this) {
		delete this;
	}
}

/* 初始化转图环境
* @param[in] ucam.db 位置
* @param[in] licenseId标记
* @param[in] 日志文件路径?
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

/* 创建Job对象
* @param[in] Job名称
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

/* 获取ODB资料工艺类别Step信息
* @param[in] tgz资料路径
*
* @return 工艺列表
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

/* 获取已经加载的层别对象
* @param[in] tgz资料路径
*
* @return 层别列表
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

/* 获取已经加载的层别对象
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

/* 加载ODB层别的方式
* @param[in] 文件路径
* @param[in] 工艺类型名称
* @param[in] 层别名称
*
* @return 加载成功的标志
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

/* 加载Gerber层别的方式
* @param[in] 文件路径
*
* @return 加载成功的标志
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

/* 加载DPF层别的方式
* @param[in] 文件路径
*
* @return 加载成功的标志
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

/* 加载所有层别
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

/* 转图参数设定 默认不裁切(裁切周边0),涨缩1;1
* @param[in] flipX 延X方向镜像(左右边,非中线)
* @param[in] flipY 延Y方向镜像(上下边,非中线)
* @param[in] rotation   旋转角度(逆时针)
* @param[in] polarity   极性
* @param[in] units      图形长度单位
* @param[in] shiftX     X方向拉伸
* @param[in] shiftY     Y方向拉伸
* @param[in] contourize 轮廓化
*/
bool UcamApi::UcamHelper::SetTransform(bool flipX, bool flipY, int rotation
	, bool polarity, int units, double shiftX, double shiftY, bool contourize) {
	return SetTransform(flipX, flipY, rotation, polarity, units, shiftX, shiftY, contourize, true, 0.0, 0.0);
}

/* 转图参数设定 默认轮廓化,涨缩1;1
* @param[in] flipX 延X方向镜像(左右边,非中线)
* @param[in] flipY 延Y方向镜像(上下边,非中线)
* @param[in] rotation   旋转角度(逆时针)
* @param[in] polarity   极性
* @param[in] units      图形长度单位
* @param[in] shiftX     X方向拉伸
* @param[in] shiftY     Y方向拉伸
* @param[in] delOutLine 是否裁切
* @param[in] delMargin  周边裁切值
*
* @return 参数设置成功标志
*/
bool UcamApi::UcamHelper::SetTransform(bool flipX, bool flipY, int rotation
	, bool polarity, int units, double shiftX, double shiftY, bool delOutLine, double delMargin) {
	return SetTransform(flipX, flipY, rotation, polarity, units, shiftX, shiftY, true, delOutLine, delMargin, delMargin);
}

/* 转图参数设定 默认涨缩1;1
* @param[in] flipX 延X方向镜像(左右边,非中线)
* @param[in] flipY 延Y方向镜像(上下边,非中线)
* @param[in] rotation   旋转角度(逆时针)
* @param[in] polarity   极性
* @param[in] units      图形长度单位
* @param[in] shiftX     X方向拉伸
* @param[in] shiftY     Y方向拉伸
* @param[in] contourize 轮廓化
* @param[in] delOutLine 是否裁切
* @param[in] delMarginX 单边左右裁切值
* @param[in] delMarginY 单边上下裁切值
*
* @return 参数设置成功标志
*/
bool UcamApi::UcamHelper::SetTransform(bool flipX, bool flipY, int rotation
	, bool polarity, int units, double shiftX, double shiftY, bool contourize
	, bool delOutLine, double delMarginX, double delMarignY) {
	return SetTransform(flipX, flipY, rotation, polarity, units, shiftX, shiftY, contourize
		, 1.0, 1.0, delOutLine, delMarginX, delMarignY);
}

/* 转图参数设定
* @param[in] flipX 延X方向镜像(左右边,非中线)
* @param[in] flipY 延Y方向镜像(上下边,非中线)
* @param[in] rotation   旋转角度(逆时针)
* @param[in] polarity   极性
* @param[in] units      图形长度单位
* @param[in] shiftX     X方向拉伸
* @param[in] shiftY     Y方向拉伸
* @param[in] contourize 轮廓化
* @param[in] scaleX     水平方向涨缩
* @param[in] scaleY		竖直方向涨缩
* @param[in] delOutLine 是否裁切
* @param[in] delMarginX 单边左右裁切值
* @param[in] delMarginY 单边上下裁切值
*
* @return 参数设置成功标志
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

/* 线宽补偿方式
* @param[in] 水平方向线宽补偿
* @param[in] 竖直方向线宽补偿
*
* @return 补偿成功标志
*/
bool UcamApi::UcamHelper::ShapeCompensation(double distanceX, double distanceY) {
	return ShapeCompensation(distanceX, distanceY, true);
}

/* 线宽补偿方式
* @param[in] 水平方向线宽补偿
* @param[in] 竖直方向线宽补偿
* @param[in] 是否按比例涨缩?
*
* @return 补偿成功标志
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

/* 设置Profile层的方法
* @param[in] profile路径
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

/* 加载边框
* @param[in] odb根目录
* @param[in] job 名称(路径)
* @param[in] step 工艺名称(路径)
*
* @return 加载成功标志
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

/* 通过aperture设置通孔属性
* @param[in] D码 (aperture 的数值)
* @param[in] 是否为矩阵?
*
* @return 设置成功的返回值
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

/* 通过aperture设置通孔属性
* @param[in] Symbol名称(aperture 的名称)
* @param[in] 是否为矩阵?
*
* @return 设置成功的返回值
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

/* 通过属性设置通孔属性
* @param[in] 属性名称
* @param[in] 属性值
* @param[in] 是否为矩阵?
*
* @return 设置成功的返回值
*/
bool UcamApi::UcamHelper::SetFiducial(String^ attrName, String^ attrValue, bool bMatrix) {
	return SetFiducial(attrName, attrValue, bMatrix, true);
}

/* 通过属性设置通孔属性
* @param[in] 属性名称
* @param[in] 属性值
* @param[in] 是否为矩阵?
* @param[in] 是否为attribute或者aperture 属性?
*
* @return 设置成功的返回值
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

/* 通过aperture设置梅花孔属性
* @param[in] aperture Center
* @param[in] D码 (aperture Number)
*
* @return 设置成功的返回值
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

/* 通过设置属性 设置梅花空信息
* @param[in] 属性名
* @param[in] 属性值
* @param[in] WildCard 为true
*
* @return 设置成功的返回值
*/
bool UcamApi::UcamHelper::SetFiducialPlum(String^ attrName, String^ attrValue) {
	return SetFiducialPlum(attrName, attrValue, true);
}

/* 通过设置属性 设置梅花空信息
* @param[in] 属性名
* @param[in] 属性值
* @param[in] 是否为WildCard ?
*
* @return 设置成功的返回值
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

/* 根据D码(aperture number)设置
* @param[in] D码
* @param[in] 字符标志
* @param[in] 字符高
* @param[in] 字符宽
*
* @return 设置成功的返回值
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

/* 根据D码(aperture number)设置
* @param[in] symbol Name
* @param[in] 字符标志
* @param[in] 字符高
* @param[in] 字符宽
*
* @return 设置成功的返回值
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

/* 根据D码(aperture number)设置
* @param[in] 属性名
* @param[in] 属性值
* @param[in] 字符标志
* @param[in] 字符高
* @param[in] 字符宽
*
* @return 设置成功的返回值
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

/* 根据D码(aperture number)设置
* @param[in] 属性名
* @param[in] 属性值
* @param[in] 字符标志
* @param[in] 字符高
* @param[in] 字符宽
* @param[in] 是否为对象属性或者aperture属性?
*
* @return 设置成功的返回值
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

/* 根据属性删除图案(目前常用于数码管)
* @param[in] 属性名
* @param[in] 属性值
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

/* 输出信息长度单位,孔位,字符,图形大小信息的方法
* @param[in] 图层位置
*
* @return 输出成功标志
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

/* 只输出对位层孔位的办法?
* @param[in] 图层位置
*
* @return 输出成功标志
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

/* 生成层别Hsah码
*
* @param[in] hash指针
*
* @return 生成成功标志
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

/* 输出GDS的转图方法
*
* @param[in] job文件路径
* @param[in] arcMargin
*
* @return 转化成功标志
*/
bool UcamApi::UcamHelper::OutputGDSII(String^ filePath) {
	return OutputGDSII(filePath, 5);
}

/* 输出GDS的转图方法
*
* @param[in] job文件路径
* @param[in] arcMargin
*
* @return 转化成功标志
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

/* 输出Gerber的转图方法
*
* @param[in] job文件路径
*
* @return 转换成功标志
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

/* 图形比对
*
* @param[in] job文件路径
* @param[in] ppi
* @param[in] 比对参数
*
* @return 比对错误个数
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

/* 释放转图对象
*/
void UcamApi::UcamHelper::Release() {
	ReleaseCurrentJob();
	m_initialized = false;
}

/* 释放当前转图对象
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

/* 获取当前执行过程的方法:初始化,导入,导出,异常
*
* @return E_PROGRESS_STATUS 结构体中的数据
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

/* 获取层属性值(从attrlist中获取相关信息)
*
* @param[in] 层名
* @param[in] 属性名
*
* @return 返回值属性值
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

/* 获取异常
*
* @param[in] e异常信息
*
* @return 返回值为空
*/
void UcamApi::UcamHelper::HandleConveterException(C_ConvException& e) {
	//String^ strMsg = CharPointer2String(m_job ? m_job->lastError() : e.getErrorDescription());
	//System::Windows::MessageBox::Show(strMsg, "Ucam-Error"
	//	, System::Windows::MessageBoxButton::OK
	//	, System::Windows::MessageBoxImage::Error);
	//throw gcnew System::Exception(strMsg);
}

/*  获取异常
*
* @param[in] e异常信息
*
* @return 返回值为空
*/
void UcamApi::UcamHelper::HandlesException(const char* e) {
	//String^ strMsg = CharPointer2String(e);
	//System::Windows::MessageBox::Show(strMsg, "Ucam-Error"
	//	, System::Windows::MessageBoxButton::OK
	//	, System::Windows::MessageBoxImage::Error);
	//throw gcnew System::Exception(strMsg);
}

/* 获取最后显示的异常
*
* @param[in] 参数为空
*
* @return 返回值异常信息
*/
String^ UcamApi::UcamHelper::GetLastError() {
	if (!m_job) return String::Empty;
	return CharPointer2String(m_job->lastError());
}

/* 数据清理
*
* @param[in] 参数为空
*
* @return 返回值为true表示清理完成,false表示清理失败
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

/* 字符串转换
*
* @param[in] string类型字符串
*
* @return C++类型字符串(char*)
*/
char* UcamApi::UcamHelper::String2CharPointer(String^ str) {
	if (String::IsNullOrEmpty(str)) return "";
	return (char*)(System::Runtime::InteropServices::Marshal::StringToHGlobalAnsi(str).ToPointer());
}

/* 字符串转换
*
* @param[in] C++类型字符串(char*)
*
* @return string类型字符串
*/
String^ UcamApi::UcamHelper::CharPointer2String(const char* pChar) {
	return System::Runtime::InteropServices::Marshal::PtrToStringAnsi(IntPtr((void*)pChar));
}

