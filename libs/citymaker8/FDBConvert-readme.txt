使用FDBConvert前，先配置FDBConvertConfiguration.xml，同时把日志级别调整到info级别（gfxlog.ini）。
直接运行FDBConvert.exe将会执行FDB数据转换以及导入后的数据校验，
运行 FDBConvert.exe -h 显示帮助。
运行 FDBConvert.exe -c 只进行数据校验，不转换数据。

FDBConvert的日志文件在同级目录下，fde_日期.log