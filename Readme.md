# Referencing unmanaged libraries in a PaaS environment

This is a proof of concept demonstrating that an `Azure Web App` can include a C++ **unmanaged** library and call functions from it.

## The unmanaged library

The project `Ugly` is an unmanaged C++ project.

The file `Ugly.cpp` contains a function called `fnUgly`:

```cpp
UGLY_API int fnUgly(void)
{
    return 42;
}
```

The file `Ugly.h` exports the `fnUgly` function:

```cpp
extern "C" {
	UGLY_API int fnUgly(void);
}
```

**You'll need to compile the DLL against the expected architecture (32 vs 64 bits)**.

## Include the unamanged library in your project

All you need to do to use an unamanged library is to copy it to your `bin` directory.

I achieved this by adding the DLLs to the project, have a look at the `Api\Ugly\x64` folder.

 - `Build Action`: `None`
 - `Copy to Output Directory`: `Copy if newer`

## Call into an unmanaged library

The code is located in `HomeController`

```chsarp
/*
* The path is relative to the bin folder.
* The function name has to match the function name in the C++ DLL,
* if required you can override this via the EntryPoint property
* in the DllImportAttribute
*/
[DllImport("Ugly\\x64\\Ugly.dll")]
public static extern int fnUgly();

/* [...] */

var value1 = fnUgly().ToString();
```

## Logging

By default Serilog will write to `D:\home\LogFiles\`, it's a hack I know but it was the fastest way to get logging up and running. You can download the log files via the Kudu console.

If you need to log somewhere else you can create a `LogPath` key in your `Web.config`.

## Troubleshooting

If you can't manage to call into your unmanaged DLL you can uncomment `LoadNativeAssemblies` in `WebApiApplication`. This will attempt to load the unmanaged DLLs manually and generate better errors in the logs.

## Process monitor

Your unmanaged DLL might depend on other unmanaged libraries that are not deployed on the target machine.

I don't know if you can enable Fusion logs in a PaaS environment or use the dependency walker. The fastest way to figure out which native modules are required is to use [Process Monitor][process-monitor] to see which files are being accessed by the process.

[process-monitor]: https://technet.microsoft.com/en-us/sysinternals/processmonitor.aspx