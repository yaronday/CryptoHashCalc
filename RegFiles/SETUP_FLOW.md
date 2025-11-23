# CryptoHashCalc Setup Flow

1. **Open Command Prompt and Navigate to the Project Directory**  
   Launch CMD and use `cd` to enter this directory where the setup files are located.

2. **Set or Update the CHC_PATH Environment Variable**  
   Run the following command to set the install path for CryptoHashCalc:  
   ```
   setCHCpath <your_desired_path>
   ```
   Replace `<your_desired_path>` with your actual desired installation folder, e.g., `C:\CryptoHashCalc`.

3. **Restart the CMD Session**  
   Close your current Command Prompt window and open a new one.  
   This ensures the updated environment variable `CHC_PATH` is available in your session.

4. **Deploy the App Files**  
   Run:
   ```
   CHCdeploy
   ```
   This will copy all necessary application files to the folder specified by your `CHC_PATH` environment variable.

5. **Generate a Ready-to-Import Registry File**  
   Prepare the registry file by processing the base configuration. Run:  
   ```
   py regfile_update_before_import
   ```
   This will create `CHCregCfgReady.reg` from `CHCregCfgBase.reg`, automatically updating the paths based on the `CHC_PATH` value.

6. **Import the Registry Configuration**  
   Double-click the generated `CHCregCfgReady.reg` file to import the settings into the Windows registry.

***

**Result:**  
The Explorer context menu will now be updated. You can right-click any file to quickly calculate its crypto-hash values using CryptoHashCalc.

***

**Note:**  
To undo the registry modifications, simply double-click on `CHCremove.reg`.

***