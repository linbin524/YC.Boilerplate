using Microsoft.VisualStudio.TestTools.UnitTesting;
using YC.Common;
using YC.CodeGenerate;
using YC.CodeGenerate.Dto;
using YC.Common.ShareUtils;
using System.Collections.Generic;

namespace YC.CodeGenerateTest
{
    [TestClass]
    public class CodeGenerateUnitTest
    {
        [TestMethod]
        public void ChangeJsonTest()
        {
            #region ��һ��ֱ��ת��д�룬��ʽ�Ƚ���
            //string dbConfigFilePath = System.Environment.CurrentDirectory + "\\DefaultConfig.json";
            //string tempJson = CodeGenerateConfig.GetConfigJson(dbConfigFilePath);
            //DbDto dbDto = CodeGenerateConfig.GetJsonList<DbDto>(tempJson);
            //dbDto.DefaultDBConnectionString = dbDto.DefaultDBConnectionString.Replace("{%dbName%}", "test");
            //bool result = CodeGenerateConfig.SetConfigJson(dbConfigFilePath, dbDto.ToJson()); 
            #endregion

            #region �ڶ��֣���ʽ�����
            //string dbConfigFilePath = System.Environment.CurrentDirectory + "\\DefaultConfig.json";
            //string tempJson = CodeGenerateConfig.GetConfigJson(dbConfigFilePath).Replace("{%dbName%}", "test");
            //bool result = CodeGenerateConfig.SetConfigJson(dbConfigFilePath, tempJson);
            #endregion
        }

        /// <summary>
        /// ����Ĭ�������Զ��������ݿ�
        /// </summary>
        [TestMethod]
        public void CreateDBTest()
        {

            using (CodeGenerateDBService codeGenerateDBRepository = new CodeGenerateDBService())
            {
                string msg = "";
                bool result = codeGenerateDBRepository.CreateDB(out msg);
                Assert.IsTrue(result);
            }

        }

        [TestMethod]
        public void CreateTableTest()
        {
            using (CodeGenerateDBService codeGenerateDBRepository = new CodeGenerateDBService())
            {
                string msg = "";
                GenerateDbTableConfig config = new GenerateDbTableConfig();
                config.GenerateDbTableEntityList = new List<string>();
                config.GenerateDbTableEntityList.Add("SysOrganization");//Ҫ���ɵı�
                config.GenerateDbTableEntityList.Add("SysAuditLog");
                config.GenerateDbTableEntityList.Add("SysUserSysOrganization");
                bool result = codeGenerateDBRepository.CreateTable(config,out msg, false);
                Assert.IsTrue(result);
            }
        }  

        /// <summary>
        /// ����������-���ɶ�Ӧ����
        /// </summary>
        [TestMethod]
        public void GenerateCodeTest()
        {
            #region ģ������ɴ���·������
            TemplateDto templateDto = new TemplateDto();
            templateDto.AddOrEditDtoFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\AddOrEditDtoTemplate.txt";
            templateDto.EntityDtoFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\EntityDtoTemplate.txt";
            templateDto.ServiceFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\AppServiceTemplate.txt";
            templateDto.IServiceFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\IAppServiceTemplate.txt";
            templateDto.VueFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\View\VueTemplate.txt";
            templateDto.TreeServiceFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\TreeAppServiceTemplate.txt";
            templateDto.TreeIServiceFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\TreeIAppServiceTemplate.txt";
            templateDto.TreeVueFilePath = System.AppContext.BaseDirectory + @"CodeTemplate\View\TreeVueTemplate.txt";
            templateDto.SaveDir = System.AppContext.BaseDirectory + "GenerateCode\\";//���ɴ��뱣��λ��
            #endregion
            #region ����Ĭ�����ɹ��
            List<GenerateCodeEnumType> wantToGenerateCodeTypeList = new List<GenerateCodeEnumType>();
            wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultAddOrEditDto);//�����༭Dto
            wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultEntityDto);//ʵ��Dto

            bool isTree = true;
            wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.OtherCode);
            if (isTree)
            {
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.TreeAppService);
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.TreeIAppService);
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.TreeVuePage);
            }
            else
            {
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultAppService);
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultIAppService);
                wantToGenerateCodeTypeList.Add(GenerateCodeEnumType.DefaultVuePage);
            }
            #endregion

            ///���δ�����ɺ�ע���޸�vue����
            ///1���ֶ��޸ķ��� typeName�Ǳ�
            //2���ֶ��޸ı༭ ��ѡ�����ݸ��ڵ�
            //3������Ľڵ� Ĭ��ʹ��Label�����ڵ�ParentId �����̶����ֶ�����
            //4�����ĸ��ڵ�ҪΪ0

            List<string> generateEntityList = new List<string>();
            generateEntityList.Add("SysOrganization");
            GenerateCodeConfig generateCodeConfig = new GenerateCodeConfig();
            generateCodeConfig.Template = templateDto;
            generateCodeConfig.WantToGenerateCodeTypeList = wantToGenerateCodeTypeList;
            generateCodeConfig.GenerateEntityList = generateEntityList;
            generateCodeConfig.IsTree = isTree;
            GenerateCodeService generateCodeRepository = new GenerateCodeService(generateCodeConfig);
            var result = generateCodeRepository.GenerateWork();
            Assert.IsTrue(result.Success);
        }

    }
}