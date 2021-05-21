Write-Output "Current Directory: $pwd`r`n"
Set-Location -Path Z:\fyp-project-repos

$android_plugin_output_dir = ".\unity-android-bluetooth-plugin\unity-android-bluetooth-plugin-library\build\intermediates\packaged-classes\debug"
$shared_models_dll = ".\BlazorSportsDataWebAsm\BlazorSportsDataWebAsm\Shared\bin\Debug\netstandard2.0\BlazorSportsDataWebAsm.Shared.dll"

$unity_plugin_libs_dir = ".\UnityAndroidBluetoothPluginPrototype\Assets\Plugins\android\"
$unity_shared_models_dir = ".\UnityAndroidBluetoothPluginPrototype\Assets\Plugins\shared";

# Write-Output "Updating Shared Models in Unity Project..."
# Copy-Item $shared_models_dll -Destination $unity_shared_models_dir
# Write-Output "`tDone!"

Write-Output "Updating Android Plugin in Unity Project..."
Get-ChildItem -Path $android_plugin_output_dir -Recurse |
    Where-Object Name -Like "classes.jar" | 
    Copy-Item -Destination "$unity_plugin_libs_dir\android-plugin.jar"
    Write-Output "`tDone!"
