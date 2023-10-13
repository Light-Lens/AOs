# A brand new entry in the AOs 2 series with a all new command-line experience.
Today, I am excited to announce the release of AOs 2.5! A brand new entry in the AOs 2 series designed two-fold: to be more efficient and powerful, and to be a powerful Developer Tool Built for Developers by a Developer.

AOs 2.5 feature a wide range of changes all of which might be hard to discuss but can be reviewed in the **Changelog**: https://github.com/Light-Lens/AOs/compare/v2.4...v2.5

## Release Notes
1. Highlights
2. New Features
3. Improvements
4. Bug Fixes
5. Usage

## Highlights
1. Developer commands
2. Run multiple commands in a single line

## New Features
1. Developer commands
2. Encrypt or Decrypt any text
3. Run multiple commands in a single line
4. Execute a command direcly from the command-line

## Improvements
1. Less crashes
2. Cleaner source code
3. Improved performance
4. Faster default-else-shell execution
5. Improved file execution from the command-line
6. Improved error handling, traceback and logging
7. Independent executable that does not require .NET Framework to run

## Bug Fixes
1. Improper error logging
2. Crashing on very edgy unexpected inputs
3. Not executing when suitable .NET Framework is not installed on the host machine

## Usage
1. Run multiple commands in a single line:
    Save your valuable time by using semicolon `;` execute multiple AOs commands in a single file. This will automatically execute those commands without you waiting for the first one to finish then execute the second manually one-by-one. AOs follows the same multi-line execution conventions as in C#.

    ```console
    AOs 2023 [Version 2.5]  (User)
    $ sfc /scannow; DISM /Online /Cleanup-image /Restorehealth
    ```