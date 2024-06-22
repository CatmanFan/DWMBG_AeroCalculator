## Aero intensity calculator for [DWMBlurGlass](https://github.com/Maplespe/DWMBlurGlass)
Based on the discussion thread [here](https://github.com/Maplespe/DWMBlurGlass/discussions/334) and [this graph and formula](https://www.desmos.com/calculator/t5wh1idedh) made by @ WackyIdeas. The C# program calculates the primary colour, secondary colour (afterglow) and blur balance values for DWMBG based on a 0 to 100 slider and can write them to a DWMBG configuration file, if provided.

### Command-line usage
Currently experimental, but the basic usage is as follows:

***dwmbgcalc.exe [-set (value)] [-refreshdwm] [-killdwm] [-restartdwmbg]***

#### Commands:
* ***-set (value)***: Changes the Aero glass intensity with a calculation similar to Windows 7's. The value must be a number between 0 and 100.
* ***-refreshdwm***: After changing Aero settings, refreshes DWM by triggering a 'theme changed' status.
* ***-killdwm***: After changing Aero settings, kills DWM and restarts any Windhawk mods enabled that have already changed titlebar button sizes.
* ***-restartdwmbg***: Starts DWMBlurGlass again if DWM has already been restarted. Useful to create as a task to start automatically, for example, in case DWM crashes.

* ***-gui***: Runs the original GUI version and sets it to start by default.
