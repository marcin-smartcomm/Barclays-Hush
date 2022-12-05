using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace UserModule_SCHEDULER___BARCLAYS____MR
{
    public class UserModuleClass_SCHEDULER___BARCLAYS____MR : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        StringParameter FILELOCATION;
        Crestron.Logos.SplusObjects.DigitalInput INITIALIZE;
        Crestron.Logos.SplusObjects.DigitalInput HOURLYCHECK;
        Crestron.Logos.SplusObjects.DigitalInput MONDAYONOFF;
        Crestron.Logos.SplusObjects.DigitalInput TUESDAYONOFF;
        Crestron.Logos.SplusObjects.DigitalInput WEDNESDAYONOFF;
        Crestron.Logos.SplusObjects.DigitalInput THURSDAYONOFF;
        Crestron.Logos.SplusObjects.DigitalInput FRIDAYONOFF;
        Crestron.Logos.SplusObjects.DigitalInput SATURDAYONOFF;
        Crestron.Logos.SplusObjects.DigitalInput SUNDAYONOFF;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> MONDAYONTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> MONDAYOFFTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> TUESDAYONTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> TUESDAYOFFTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> WEDNESDAYONTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> WEDNESDAYOFFTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> THURSDAYONTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> THURSDAYOFFTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> FRIDAYONTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> FRIDAYOFFTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> SATURDAYONTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> SATURDAYOFFTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> SUNDAYONTIME;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> SUNDAYOFFTIME;
        Crestron.Logos.SplusObjects.DigitalOutput MONDAYONFB;
        Crestron.Logos.SplusObjects.DigitalOutput TUESDAYONFB;
        Crestron.Logos.SplusObjects.DigitalOutput WEDNESDAYONFB;
        Crestron.Logos.SplusObjects.DigitalOutput THURSDAYONFB;
        Crestron.Logos.SplusObjects.DigitalOutput FRIDAYONFB;
        Crestron.Logos.SplusObjects.DigitalOutput SATURDAYONFB;
        Crestron.Logos.SplusObjects.DigitalOutput SUNDAYONFB;
        Crestron.Logos.SplusObjects.DigitalOutput SCREENSON;
        Crestron.Logos.SplusObjects.DigitalOutput SCREENSOFF;
        Crestron.Logos.SplusObjects.StringOutput MONDAYONTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput MONDAYOFFTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput TUESDAYONTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput TUESDAYOFFTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput WEDNESDAYONTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput WEDNESDAYOFFTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput THURSDAYONTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput THURSDAYOFFTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput FRIDAYONTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput FRIDAYOFFTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput SATURDAYONTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput SATURDAYOFFTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput SUNDAYONTIMEFB;
        Crestron.Logos.SplusObjects.StringOutput SUNDAYOFFTIMEFB;
        CrestronString SBUF;
        CrestronString SCHEDULEDATA;
        CrestronString SCHEDULEDATATEMPLATE;
        CrestronString TODAY__DOLLAR__;
        ushort I = 0;
        ushort X = 0;
        short NFILEHANDLE = 0;
        private void CREATEDATATEMPLATE (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 36;
            SCHEDULEDATATEMPLATE  .UpdateValue ( _SplusNVRAM.DAYS [ 0 ] + "-" + Functions.ItoA (  (int) ( _SplusNVRAM.DAYSTATE[ 0 ] ) ) + "/" + _SplusNVRAM.ONTIME [ 0 ] + ":00/" + _SplusNVRAM.OFFTIME [ 0 ] + ":00\r\n"  ) ; 
            __context__.SourceCodeLine = 37;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)6; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 39;
                SCHEDULEDATATEMPLATE  .UpdateValue ( SCHEDULEDATATEMPLATE + _SplusNVRAM.DAYS [ I ] + "-" + Functions.ItoA (  (int) ( _SplusNVRAM.DAYSTATE[ I ] ) ) + "/" + _SplusNVRAM.ONTIME [ I ] + ":00/" + _SplusNVRAM.OFFTIME [ I ] + ":00\r\n"  ) ; 
                __context__.SourceCodeLine = 37;
                } 
            
            
            }
            
        object INITIALIZE_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                CrestronString DAYSTATE__DOLLAR__;
                DAYSTATE__DOLLAR__  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 2, this );
                
                ushort STATEPOS = 0;
                ushort TIMEPOS = 0;
                
                
                __context__.SourceCodeLine = 48;
                _SplusNVRAM.DAYS [ 0 ]  .UpdateValue ( "Mon"  ) ; 
                __context__.SourceCodeLine = 49;
                _SplusNVRAM.DAYS [ 1 ]  .UpdateValue ( "Tue"  ) ; 
                __context__.SourceCodeLine = 50;
                _SplusNVRAM.DAYS [ 2 ]  .UpdateValue ( "Wed"  ) ; 
                __context__.SourceCodeLine = 51;
                _SplusNVRAM.DAYS [ 3 ]  .UpdateValue ( "Thu"  ) ; 
                __context__.SourceCodeLine = 52;
                _SplusNVRAM.DAYS [ 4 ]  .UpdateValue ( "Fri"  ) ; 
                __context__.SourceCodeLine = 53;
                _SplusNVRAM.DAYS [ 5 ]  .UpdateValue ( "Sat"  ) ; 
                __context__.SourceCodeLine = 54;
                _SplusNVRAM.DAYS [ 6 ]  .UpdateValue ( "Sun"  ) ; 
                __context__.SourceCodeLine = 56;
                StartFileOperations ( ) ; 
                __context__.SourceCodeLine = 58;
                NFILEHANDLE = (short) ( FileOpen( FILELOCATION  ,(ushort) (0 | 16384) ) ) ; 
                __context__.SourceCodeLine = 60;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( NFILEHANDLE >= 0 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 64;
                    while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( FileRead( (short)( NFILEHANDLE ) , SBUF , (ushort)( 4096 ) ) > 0 ))  ) ) 
                        {
                        __context__.SourceCodeLine = 65;
                        SCHEDULEDATA  .UpdateValue ( SBUF  ) ; 
                        __context__.SourceCodeLine = 64;
                        }
                    
                    __context__.SourceCodeLine = 67;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (FileClose( (short)( NFILEHANDLE ) ) != 0))  ) ) 
                        {
                        __context__.SourceCodeLine = 69;
                        Print( "Error closing file\r\n") ; 
                        }
                    
                    } 
                
                __context__.SourceCodeLine = 72;
                EndFileOperations ( ) ; 
                __context__.SourceCodeLine = 74;
                ushort __FN_FORSTART_VAL__1 = (ushort) ( 0 ) ;
                ushort __FN_FOREND_VAL__1 = (ushort)6; 
                int __FN_FORSTEP_VAL__1 = (int)1; 
                for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                    { 
                    __context__.SourceCodeLine = 77;
                    STATEPOS = (ushort) ( (Functions.Find( _SplusNVRAM.DAYS[ I ] , SCHEDULEDATA ) + 4) ) ; 
                    __context__.SourceCodeLine = 78;
                    _SplusNVRAM.DAYSTATE [ I] = (ushort) ( Functions.Atoi( Functions.Mid( SCHEDULEDATA , (int)( STATEPOS ) , (int)( 1 ) ) ) ) ; 
                    __context__.SourceCodeLine = 83;
                    TIMEPOS = (ushort) ( (STATEPOS + 2) ) ; 
                    __context__.SourceCodeLine = 84;
                    _SplusNVRAM.ONTIME [ I ]  .UpdateValue ( Functions.Mid ( SCHEDULEDATA ,  (int) ( TIMEPOS ) ,  (int) ( 2 ) )  ) ; 
                    __context__.SourceCodeLine = 87;
                    TIMEPOS = (ushort) ( (TIMEPOS + 6) ) ; 
                    __context__.SourceCodeLine = 88;
                    _SplusNVRAM.OFFTIME [ I ]  .UpdateValue ( Functions.Mid ( SCHEDULEDATA ,  (int) ( TIMEPOS ) ,  (int) ( 2 ) )  ) ; 
                    __context__.SourceCodeLine = 74;
                    } 
                
                __context__.SourceCodeLine = 91;
                _SplusNVRAM.MONDAYSTATE = (ushort) ( _SplusNVRAM.DAYSTATE[ 0 ] ) ; 
                __context__.SourceCodeLine = 92;
                _SplusNVRAM.TUESDAYSTATE = (ushort) ( _SplusNVRAM.DAYSTATE[ 1 ] ) ; 
                __context__.SourceCodeLine = 93;
                _SplusNVRAM.WEDNESDAYSTATE = (ushort) ( _SplusNVRAM.DAYSTATE[ 2 ] ) ; 
                __context__.SourceCodeLine = 94;
                _SplusNVRAM.THURSDAYSTATE = (ushort) ( _SplusNVRAM.DAYSTATE[ 3 ] ) ; 
                __context__.SourceCodeLine = 95;
                _SplusNVRAM.FRIDAYSTATE = (ushort) ( _SplusNVRAM.DAYSTATE[ 4 ] ) ; 
                __context__.SourceCodeLine = 96;
                _SplusNVRAM.SATURDAYSTATE = (ushort) ( _SplusNVRAM.DAYSTATE[ 5 ] ) ; 
                __context__.SourceCodeLine = 97;
                _SplusNVRAM.SUNDAYSTATE = (ushort) ( _SplusNVRAM.DAYSTATE[ 6 ] ) ; 
                __context__.SourceCodeLine = 99;
                MONDAYONFB  .Value = (ushort) ( _SplusNVRAM.MONDAYSTATE ) ; 
                __context__.SourceCodeLine = 100;
                TUESDAYONFB  .Value = (ushort) ( _SplusNVRAM.TUESDAYSTATE ) ; 
                __context__.SourceCodeLine = 101;
                WEDNESDAYONFB  .Value = (ushort) ( _SplusNVRAM.WEDNESDAYSTATE ) ; 
                __context__.SourceCodeLine = 102;
                THURSDAYONFB  .Value = (ushort) ( _SplusNVRAM.THURSDAYSTATE ) ; 
                __context__.SourceCodeLine = 103;
                FRIDAYONFB  .Value = (ushort) ( _SplusNVRAM.FRIDAYSTATE ) ; 
                __context__.SourceCodeLine = 104;
                SATURDAYONFB  .Value = (ushort) ( _SplusNVRAM.SATURDAYSTATE ) ; 
                __context__.SourceCodeLine = 105;
                SUNDAYONFB  .Value = (ushort) ( _SplusNVRAM.SUNDAYSTATE ) ; 
                __context__.SourceCodeLine = 107;
                MONDAYONTIMEFB  .UpdateValue ( _SplusNVRAM.ONTIME [ 0 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 108;
                MONDAYOFFTIMEFB  .UpdateValue ( _SplusNVRAM.OFFTIME [ 0 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 109;
                TUESDAYONTIMEFB  .UpdateValue ( _SplusNVRAM.ONTIME [ 1 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 110;
                TUESDAYOFFTIMEFB  .UpdateValue ( _SplusNVRAM.OFFTIME [ 1 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 111;
                WEDNESDAYONTIMEFB  .UpdateValue ( _SplusNVRAM.ONTIME [ 2 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 112;
                WEDNESDAYOFFTIMEFB  .UpdateValue ( _SplusNVRAM.OFFTIME [ 2 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 113;
                THURSDAYONTIMEFB  .UpdateValue ( _SplusNVRAM.ONTIME [ 3 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 114;
                THURSDAYOFFTIMEFB  .UpdateValue ( _SplusNVRAM.OFFTIME [ 3 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 115;
                FRIDAYONTIMEFB  .UpdateValue ( _SplusNVRAM.ONTIME [ 4 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 116;
                FRIDAYOFFTIMEFB  .UpdateValue ( _SplusNVRAM.OFFTIME [ 4 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 117;
                SATURDAYONTIMEFB  .UpdateValue ( _SplusNVRAM.ONTIME [ 5 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 118;
                SATURDAYOFFTIMEFB  .UpdateValue ( _SplusNVRAM.OFFTIME [ 5 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 119;
                SUNDAYONTIMEFB  .UpdateValue ( _SplusNVRAM.ONTIME [ 6 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 120;
                SUNDAYOFFTIMEFB  .UpdateValue ( _SplusNVRAM.OFFTIME [ 6 ] + ":00"  ) ; 
                __context__.SourceCodeLine = 122;
                CREATEDATATEMPLATE (  __context__  ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    private void UPDATEFILE (  SplusExecutionContext __context__ ) 
        { 
        
        __context__.SourceCodeLine = 127;
        CREATEDATATEMPLATE (  __context__  ) ; 
        __context__.SourceCodeLine = 129;
        StartFileOperations ( ) ; 
        __context__.SourceCodeLine = 131;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (FileDelete( "\\NVRAM\\SchedulerTimes.txt" ) != 0))  ) ) 
            {
            __context__.SourceCodeLine = 133;
            Print( "Error deleting file\r\n") ; 
            }
        
        __context__.SourceCodeLine = 135;
        NFILEHANDLE = (short) ( FileOpen( "\\NVRAM\\SchedulerTimes.txt" ,(ushort) ((256 | 1) | 16384) ) ) ; 
        __context__.SourceCodeLine = 136;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( NFILEHANDLE >= 0 ))  ) ) 
            { 
            __context__.SourceCodeLine = 138;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( FileWrite( (short)( NFILEHANDLE ) , SCHEDULEDATATEMPLATE , (ushort)( 4096 ) ) > 0 ))  ) ) 
                {
                __context__.SourceCodeLine = 139;
                Print( "Written to file:\r\n{0}", SCHEDULEDATATEMPLATE ) ; 
                }
            
            __context__.SourceCodeLine = 140;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (FileClose( (short)( NFILEHANDLE ) ) != 0))  ) ) 
                {
                __context__.SourceCodeLine = 141;
                Print( "Error closing file \r\n") ; 
                }
            
            } 
        
        __context__.SourceCodeLine = 144;
        EndFileOperations ( ) ; 
        
        }
        
    object HOURLYCHECK_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 150;
            TODAY__DOLLAR__  .UpdateValue ( Functions.Day ( )  ) ; 
            __context__.SourceCodeLine = 152;
            ushort __FN_FORSTART_VAL__1 = (ushort) ( 0 ) ;
            ushort __FN_FOREND_VAL__1 = (ushort)6; 
            int __FN_FORSTEP_VAL__1 = (int)1; 
            for ( I  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (I  >= __FN_FORSTART_VAL__1) && (I  <= __FN_FOREND_VAL__1) ) : ( (I  <= __FN_FORSTART_VAL__1) && (I  >= __FN_FOREND_VAL__1) ) ; I  += (ushort)__FN_FORSTEP_VAL__1) 
                { 
                __context__.SourceCodeLine = 154;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Left( TODAY__DOLLAR__ , (int)( 3 ) ) == _SplusNVRAM.DAYS[ I ]))  ) ) 
                    { 
                    __context__.SourceCodeLine = 156;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.DAYSTATE[ I ] == 1))  ) ) 
                        { 
                        __context__.SourceCodeLine = 158;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Atoi( _SplusNVRAM.ONTIME[ I ] ) == Functions.GetHourNum()))  ) ) 
                            { 
                            __context__.SourceCodeLine = 160;
                            Functions.Pulse ( 1, SCREENSON ) ; 
                            } 
                        
                        __context__.SourceCodeLine = 163;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Atoi( _SplusNVRAM.OFFTIME[ I ] ) == Functions.GetHourNum()))  ) ) 
                            { 
                            __context__.SourceCodeLine = 165;
                            Functions.Pulse ( 1, SCREENSOFF ) ; 
                            } 
                        
                        } 
                    
                    } 
                
                __context__.SourceCodeLine = 152;
                } 
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object MONDAYONOFF_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 174;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.MONDAYSTATE == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 175;
            _SplusNVRAM.MONDAYSTATE = (ushort) ( 0 ) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 176;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.MONDAYSTATE == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 177;
                _SplusNVRAM.MONDAYSTATE = (ushort) ( 1 ) ; 
                } 
            
            }
        
        __context__.SourceCodeLine = 179;
        MONDAYONFB  .Value = (ushort) ( _SplusNVRAM.MONDAYSTATE ) ; 
        __context__.SourceCodeLine = 180;
        _SplusNVRAM.DAYSTATE [ 0] = (ushort) ( _SplusNVRAM.MONDAYSTATE ) ; 
        __context__.SourceCodeLine = 181;
        UPDATEFILE (  __context__  ) ; 
        __context__.SourceCodeLine = 183;
        Functions.Delay (  (int) ( 25 ) ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object TUESDAYONOFF_OnPush_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 188;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.TUESDAYSTATE == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 189;
            _SplusNVRAM.TUESDAYSTATE = (ushort) ( 0 ) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 190;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.TUESDAYSTATE == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 191;
                _SplusNVRAM.TUESDAYSTATE = (ushort) ( 1 ) ; 
                } 
            
            }
        
        __context__.SourceCodeLine = 193;
        TUESDAYONFB  .Value = (ushort) ( _SplusNVRAM.TUESDAYSTATE ) ; 
        __context__.SourceCodeLine = 194;
        _SplusNVRAM.DAYSTATE [ 1] = (ushort) ( _SplusNVRAM.TUESDAYSTATE ) ; 
        __context__.SourceCodeLine = 195;
        UPDATEFILE (  __context__  ) ; 
        __context__.SourceCodeLine = 197;
        Functions.Delay (  (int) ( 25 ) ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object WEDNESDAYONOFF_OnPush_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 203;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.WEDNESDAYSTATE == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 204;
            _SplusNVRAM.WEDNESDAYSTATE = (ushort) ( 0 ) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 205;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.WEDNESDAYSTATE == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 206;
                _SplusNVRAM.WEDNESDAYSTATE = (ushort) ( 1 ) ; 
                } 
            
            }
        
        __context__.SourceCodeLine = 208;
        WEDNESDAYONFB  .Value = (ushort) ( _SplusNVRAM.WEDNESDAYSTATE ) ; 
        __context__.SourceCodeLine = 209;
        _SplusNVRAM.DAYSTATE [ 2] = (ushort) ( _SplusNVRAM.WEDNESDAYSTATE ) ; 
        __context__.SourceCodeLine = 210;
        UPDATEFILE (  __context__  ) ; 
        __context__.SourceCodeLine = 212;
        Functions.Delay (  (int) ( 25 ) ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object THURSDAYONOFF_OnPush_5 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 218;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.THURSDAYSTATE == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 219;
            _SplusNVRAM.THURSDAYSTATE = (ushort) ( 0 ) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 220;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.THURSDAYSTATE == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 221;
                _SplusNVRAM.THURSDAYSTATE = (ushort) ( 1 ) ; 
                } 
            
            }
        
        __context__.SourceCodeLine = 223;
        THURSDAYONFB  .Value = (ushort) ( _SplusNVRAM.THURSDAYSTATE ) ; 
        __context__.SourceCodeLine = 224;
        _SplusNVRAM.DAYSTATE [ 3] = (ushort) ( _SplusNVRAM.THURSDAYSTATE ) ; 
        __context__.SourceCodeLine = 225;
        UPDATEFILE (  __context__  ) ; 
        __context__.SourceCodeLine = 227;
        Functions.Delay (  (int) ( 25 ) ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object FRIDAYONOFF_OnPush_6 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 233;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.FRIDAYSTATE == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 234;
            _SplusNVRAM.FRIDAYSTATE = (ushort) ( 0 ) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 235;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.FRIDAYSTATE == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 236;
                _SplusNVRAM.FRIDAYSTATE = (ushort) ( 1 ) ; 
                } 
            
            }
        
        __context__.SourceCodeLine = 238;
        FRIDAYONFB  .Value = (ushort) ( _SplusNVRAM.FRIDAYSTATE ) ; 
        __context__.SourceCodeLine = 239;
        _SplusNVRAM.DAYSTATE [ 4] = (ushort) ( _SplusNVRAM.FRIDAYSTATE ) ; 
        __context__.SourceCodeLine = 240;
        UPDATEFILE (  __context__  ) ; 
        __context__.SourceCodeLine = 242;
        Functions.Delay (  (int) ( 25 ) ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SATURDAYONOFF_OnPush_7 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 248;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.SATURDAYSTATE == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 249;
            _SplusNVRAM.SATURDAYSTATE = (ushort) ( 0 ) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 250;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.SATURDAYSTATE == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 251;
                _SplusNVRAM.SATURDAYSTATE = (ushort) ( 1 ) ; 
                } 
            
            }
        
        __context__.SourceCodeLine = 253;
        SATURDAYONFB  .Value = (ushort) ( _SplusNVRAM.SATURDAYSTATE ) ; 
        __context__.SourceCodeLine = 254;
        _SplusNVRAM.DAYSTATE [ 5] = (ushort) ( _SplusNVRAM.SATURDAYSTATE ) ; 
        __context__.SourceCodeLine = 255;
        UPDATEFILE (  __context__  ) ; 
        __context__.SourceCodeLine = 257;
        Functions.Delay (  (int) ( 25 ) ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SUNDAYONOFF_OnPush_8 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 263;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.SUNDAYSTATE == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 264;
            _SplusNVRAM.SUNDAYSTATE = (ushort) ( 0 ) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 265;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.SUNDAYSTATE == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 266;
                _SplusNVRAM.SUNDAYSTATE = (ushort) ( 1 ) ; 
                } 
            
            }
        
        __context__.SourceCodeLine = 268;
        SUNDAYONFB  .Value = (ushort) ( _SplusNVRAM.SUNDAYSTATE ) ; 
        __context__.SourceCodeLine = 269;
        _SplusNVRAM.DAYSTATE [ 6] = (ushort) ( _SplusNVRAM.SUNDAYSTATE ) ; 
        __context__.SourceCodeLine = 270;
        UPDATEFILE (  __context__  ) ; 
        __context__.SourceCodeLine = 272;
        Functions.Delay (  (int) ( 25 ) ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

private void UPDATEUI (  SplusExecutionContext __context__, CrestronString WHICHDAY , CrestronString WHATSTATE , ushort WHICHHOUR ) 
    { 
    CrestronString LEADINGZERO;
    LEADINGZERO  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
    
    CrestronString TOUI;
    TOUI  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 500, this );
    
    
    __context__.SourceCodeLine = 280;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( WHICHHOUR < 10 ))  ) ) 
        { 
        __context__.SourceCodeLine = 281;
        LEADINGZERO  .UpdateValue ( "0"  ) ; 
        } 
    
    else 
        { 
        __context__.SourceCodeLine = 283;
        LEADINGZERO  .UpdateValue ( ""  ) ; 
        } 
    
    __context__.SourceCodeLine = 285;
    TOUI  .UpdateValue ( LEADINGZERO + Functions.ItoA (  (int) ( WHICHHOUR ) ) + ":00"  ) ; 
    __context__.SourceCodeLine = 287;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHICHDAY == "Monday"))  ) ) 
        { 
        __context__.SourceCodeLine = 289;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "On"))  ) ) 
            { 
            __context__.SourceCodeLine = 291;
            MONDAYONTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        __context__.SourceCodeLine = 293;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "Off"))  ) ) 
            { 
            __context__.SourceCodeLine = 295;
            MONDAYOFFTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        } 
    
    __context__.SourceCodeLine = 299;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHICHDAY == "Tuesday"))  ) ) 
        { 
        __context__.SourceCodeLine = 301;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "On"))  ) ) 
            { 
            __context__.SourceCodeLine = 303;
            TUESDAYONTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        __context__.SourceCodeLine = 305;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "Off"))  ) ) 
            { 
            __context__.SourceCodeLine = 307;
            TUESDAYOFFTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        } 
    
    __context__.SourceCodeLine = 311;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHICHDAY == "Wednesday"))  ) ) 
        { 
        __context__.SourceCodeLine = 313;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "On"))  ) ) 
            { 
            __context__.SourceCodeLine = 315;
            WEDNESDAYONTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        __context__.SourceCodeLine = 317;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "Off"))  ) ) 
            { 
            __context__.SourceCodeLine = 319;
            WEDNESDAYOFFTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        } 
    
    __context__.SourceCodeLine = 323;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHICHDAY == "Thursday"))  ) ) 
        { 
        __context__.SourceCodeLine = 325;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "On"))  ) ) 
            { 
            __context__.SourceCodeLine = 327;
            THURSDAYONTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        __context__.SourceCodeLine = 329;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "Off"))  ) ) 
            { 
            __context__.SourceCodeLine = 331;
            THURSDAYOFFTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        } 
    
    __context__.SourceCodeLine = 335;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHICHDAY == "Friday"))  ) ) 
        { 
        __context__.SourceCodeLine = 337;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "On"))  ) ) 
            { 
            __context__.SourceCodeLine = 339;
            FRIDAYONTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        __context__.SourceCodeLine = 341;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "Off"))  ) ) 
            { 
            __context__.SourceCodeLine = 343;
            FRIDAYOFFTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        } 
    
    __context__.SourceCodeLine = 347;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHICHDAY == "Saturday"))  ) ) 
        { 
        __context__.SourceCodeLine = 349;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "On"))  ) ) 
            { 
            __context__.SourceCodeLine = 351;
            SATURDAYONTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        __context__.SourceCodeLine = 353;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "Off"))  ) ) 
            { 
            __context__.SourceCodeLine = 355;
            SATURDAYOFFTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        } 
    
    __context__.SourceCodeLine = 359;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHICHDAY == "Sunday"))  ) ) 
        { 
        __context__.SourceCodeLine = 361;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "On"))  ) ) 
            { 
            __context__.SourceCodeLine = 363;
            SUNDAYONTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        __context__.SourceCodeLine = 365;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (WHATSTATE == "Off"))  ) ) 
            { 
            __context__.SourceCodeLine = 367;
            SUNDAYOFFTIMEFB  .UpdateValue ( TOUI  ) ; 
            } 
        
        } 
    
    
    }
    
private void UPDATEONTTIME (  SplusExecutionContext __context__, ushort WHATDAY , ushort WHATTIME ) 
    { 
    CrestronString LEADINGZERO;
    LEADINGZERO  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 2, this );
    
    
    __context__.SourceCodeLine = 376;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( WHATTIME < 10 ))  ) ) 
        { 
        __context__.SourceCodeLine = 377;
        LEADINGZERO  .UpdateValue ( "0"  ) ; 
        } 
    
    else 
        { 
        __context__.SourceCodeLine = 379;
        LEADINGZERO  .UpdateValue ( ""  ) ; 
        } 
    
    __context__.SourceCodeLine = 382;
    _SplusNVRAM.ONTIME [ WHATDAY ]  .UpdateValue ( LEADINGZERO + Functions.ItoA (  (int) ( WHATTIME ) )  ) ; 
    
    }
    
private void UPDATEOFFTTIME (  SplusExecutionContext __context__, ushort WHATDAY , ushort WHATTIME ) 
    { 
    CrestronString LEADINGZERO;
    LEADINGZERO  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 2, this );
    
    
    __context__.SourceCodeLine = 389;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( WHATTIME < 10 ))  ) ) 
        { 
        __context__.SourceCodeLine = 390;
        LEADINGZERO  .UpdateValue ( "0"  ) ; 
        } 
    
    else 
        { 
        __context__.SourceCodeLine = 392;
        LEADINGZERO  .UpdateValue ( ""  ) ; 
        } 
    
    __context__.SourceCodeLine = 395;
    _SplusNVRAM.OFFTIME [ WHATDAY ]  .UpdateValue ( LEADINGZERO + Functions.ItoA (  (int) ( WHATTIME ) )  ) ; 
    
    }
    
object MONDAYONTIME_OnPush_9 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 400;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 402;
        UPDATEUI (  __context__ , "Monday", "On", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 403;
        UPDATEONTTIME (  __context__ , (ushort)( 0 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 404;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MONDAYOFFTIME_OnPush_10 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 409;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 411;
        UPDATEUI (  __context__ , "Monday", "Off", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 412;
        UPDATEOFFTTIME (  __context__ , (ushort)( 0 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 413;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object TUESDAYONTIME_OnPush_11 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 418;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 420;
        UPDATEUI (  __context__ , "Tuesday", "On", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 421;
        UPDATEONTTIME (  __context__ , (ushort)( 1 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 422;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object TUESDAYOFFTIME_OnPush_12 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 427;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 429;
        UPDATEUI (  __context__ , "Tuesday", "Off", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 430;
        UPDATEOFFTTIME (  __context__ , (ushort)( 1 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 431;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object WEDNESDAYONTIME_OnPush_13 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 436;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 438;
        UPDATEUI (  __context__ , "Wednesday", "On", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 439;
        UPDATEONTTIME (  __context__ , (ushort)( 2 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 440;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object WEDNESDAYOFFTIME_OnPush_14 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 445;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 447;
        UPDATEUI (  __context__ , "Wednesday", "Off", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 448;
        UPDATEOFFTTIME (  __context__ , (ushort)( 2 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 449;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object THURSDAYONTIME_OnPush_15 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 454;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 456;
        UPDATEUI (  __context__ , "Thursday", "On", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 457;
        UPDATEONTTIME (  __context__ , (ushort)( 3 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 458;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object THURSDAYOFFTIME_OnPush_16 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 463;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 465;
        UPDATEUI (  __context__ , "Thursday", "Off", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 466;
        UPDATEOFFTTIME (  __context__ , (ushort)( 3 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 467;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object FRIDAYONTIME_OnPush_17 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 472;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 474;
        UPDATEUI (  __context__ , "Friday", "On", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 475;
        UPDATEONTTIME (  __context__ , (ushort)( 4 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 476;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object FRIDAYOFFTIME_OnPush_18 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 481;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 483;
        UPDATEUI (  __context__ , "Friday", "Off", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 484;
        UPDATEOFFTTIME (  __context__ , (ushort)( 4 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 485;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SATURDAYONTIME_OnPush_19 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 490;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 492;
        UPDATEUI (  __context__ , "Saturday", "On", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 493;
        UPDATEONTTIME (  __context__ , (ushort)( 5 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 494;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SATURDAYOFFTIME_OnPush_20 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 499;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 501;
        UPDATEUI (  __context__ , "Saturday", "Off", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 502;
        UPDATEOFFTTIME (  __context__ , (ushort)( 5 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 503;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SUNDAYONTIME_OnPush_21 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 508;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 510;
        UPDATEUI (  __context__ , "Sunday", "On", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 511;
        UPDATEONTTIME (  __context__ , (ushort)( 6 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 512;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SUNDAYOFFTIME_OnPush_22 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 517;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 519;
        UPDATEUI (  __context__ , "Sunday", "Off", (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 520;
        UPDATEOFFTTIME (  __context__ , (ushort)( 6 ), (ushort)( (X - 1) )) ; 
        __context__.SourceCodeLine = 521;
        UPDATEFILE (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}


public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    _SplusNVRAM.DAYSTATE  = new ushort[ 7 ];
    SBUF  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4096, this );
    SCHEDULEDATA  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4096, this );
    SCHEDULEDATATEMPLATE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4096, this );
    TODAY__DOLLAR__  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 20, this );
    _SplusNVRAM.DAYS  = new CrestronString[ 7 ];
    for( uint i = 0; i < 7; i++ )
        _SplusNVRAM.DAYS [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 4, this );
    _SplusNVRAM.ONTIME  = new CrestronString[ 7 ];
    for( uint i = 0; i < 7; i++ )
        _SplusNVRAM.ONTIME [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 3, this );
    _SplusNVRAM.OFFTIME  = new CrestronString[ 7 ];
    for( uint i = 0; i < 7; i++ )
        _SplusNVRAM.OFFTIME [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 3, this );
    
    INITIALIZE = new Crestron.Logos.SplusObjects.DigitalInput( INITIALIZE__DigitalInput__, this );
    m_DigitalInputList.Add( INITIALIZE__DigitalInput__, INITIALIZE );
    
    HOURLYCHECK = new Crestron.Logos.SplusObjects.DigitalInput( HOURLYCHECK__DigitalInput__, this );
    m_DigitalInputList.Add( HOURLYCHECK__DigitalInput__, HOURLYCHECK );
    
    MONDAYONOFF = new Crestron.Logos.SplusObjects.DigitalInput( MONDAYONOFF__DigitalInput__, this );
    m_DigitalInputList.Add( MONDAYONOFF__DigitalInput__, MONDAYONOFF );
    
    TUESDAYONOFF = new Crestron.Logos.SplusObjects.DigitalInput( TUESDAYONOFF__DigitalInput__, this );
    m_DigitalInputList.Add( TUESDAYONOFF__DigitalInput__, TUESDAYONOFF );
    
    WEDNESDAYONOFF = new Crestron.Logos.SplusObjects.DigitalInput( WEDNESDAYONOFF__DigitalInput__, this );
    m_DigitalInputList.Add( WEDNESDAYONOFF__DigitalInput__, WEDNESDAYONOFF );
    
    THURSDAYONOFF = new Crestron.Logos.SplusObjects.DigitalInput( THURSDAYONOFF__DigitalInput__, this );
    m_DigitalInputList.Add( THURSDAYONOFF__DigitalInput__, THURSDAYONOFF );
    
    FRIDAYONOFF = new Crestron.Logos.SplusObjects.DigitalInput( FRIDAYONOFF__DigitalInput__, this );
    m_DigitalInputList.Add( FRIDAYONOFF__DigitalInput__, FRIDAYONOFF );
    
    SATURDAYONOFF = new Crestron.Logos.SplusObjects.DigitalInput( SATURDAYONOFF__DigitalInput__, this );
    m_DigitalInputList.Add( SATURDAYONOFF__DigitalInput__, SATURDAYONOFF );
    
    SUNDAYONOFF = new Crestron.Logos.SplusObjects.DigitalInput( SUNDAYONOFF__DigitalInput__, this );
    m_DigitalInputList.Add( SUNDAYONOFF__DigitalInput__, SUNDAYONOFF );
    
    MONDAYONTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        MONDAYONTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( MONDAYONTIME__DigitalInput__ + i, MONDAYONTIME__DigitalInput__, this );
        m_DigitalInputList.Add( MONDAYONTIME__DigitalInput__ + i, MONDAYONTIME[i+1] );
    }
    
    MONDAYOFFTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        MONDAYOFFTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( MONDAYOFFTIME__DigitalInput__ + i, MONDAYOFFTIME__DigitalInput__, this );
        m_DigitalInputList.Add( MONDAYOFFTIME__DigitalInput__ + i, MONDAYOFFTIME[i+1] );
    }
    
    TUESDAYONTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        TUESDAYONTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( TUESDAYONTIME__DigitalInput__ + i, TUESDAYONTIME__DigitalInput__, this );
        m_DigitalInputList.Add( TUESDAYONTIME__DigitalInput__ + i, TUESDAYONTIME[i+1] );
    }
    
    TUESDAYOFFTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        TUESDAYOFFTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( TUESDAYOFFTIME__DigitalInput__ + i, TUESDAYOFFTIME__DigitalInput__, this );
        m_DigitalInputList.Add( TUESDAYOFFTIME__DigitalInput__ + i, TUESDAYOFFTIME[i+1] );
    }
    
    WEDNESDAYONTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        WEDNESDAYONTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( WEDNESDAYONTIME__DigitalInput__ + i, WEDNESDAYONTIME__DigitalInput__, this );
        m_DigitalInputList.Add( WEDNESDAYONTIME__DigitalInput__ + i, WEDNESDAYONTIME[i+1] );
    }
    
    WEDNESDAYOFFTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        WEDNESDAYOFFTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( WEDNESDAYOFFTIME__DigitalInput__ + i, WEDNESDAYOFFTIME__DigitalInput__, this );
        m_DigitalInputList.Add( WEDNESDAYOFFTIME__DigitalInput__ + i, WEDNESDAYOFFTIME[i+1] );
    }
    
    THURSDAYONTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        THURSDAYONTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( THURSDAYONTIME__DigitalInput__ + i, THURSDAYONTIME__DigitalInput__, this );
        m_DigitalInputList.Add( THURSDAYONTIME__DigitalInput__ + i, THURSDAYONTIME[i+1] );
    }
    
    THURSDAYOFFTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        THURSDAYOFFTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( THURSDAYOFFTIME__DigitalInput__ + i, THURSDAYOFFTIME__DigitalInput__, this );
        m_DigitalInputList.Add( THURSDAYOFFTIME__DigitalInput__ + i, THURSDAYOFFTIME[i+1] );
    }
    
    FRIDAYONTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        FRIDAYONTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( FRIDAYONTIME__DigitalInput__ + i, FRIDAYONTIME__DigitalInput__, this );
        m_DigitalInputList.Add( FRIDAYONTIME__DigitalInput__ + i, FRIDAYONTIME[i+1] );
    }
    
    FRIDAYOFFTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        FRIDAYOFFTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( FRIDAYOFFTIME__DigitalInput__ + i, FRIDAYOFFTIME__DigitalInput__, this );
        m_DigitalInputList.Add( FRIDAYOFFTIME__DigitalInput__ + i, FRIDAYOFFTIME[i+1] );
    }
    
    SATURDAYONTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        SATURDAYONTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( SATURDAYONTIME__DigitalInput__ + i, SATURDAYONTIME__DigitalInput__, this );
        m_DigitalInputList.Add( SATURDAYONTIME__DigitalInput__ + i, SATURDAYONTIME[i+1] );
    }
    
    SATURDAYOFFTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        SATURDAYOFFTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( SATURDAYOFFTIME__DigitalInput__ + i, SATURDAYOFFTIME__DigitalInput__, this );
        m_DigitalInputList.Add( SATURDAYOFFTIME__DigitalInput__ + i, SATURDAYOFFTIME[i+1] );
    }
    
    SUNDAYONTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        SUNDAYONTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( SUNDAYONTIME__DigitalInput__ + i, SUNDAYONTIME__DigitalInput__, this );
        m_DigitalInputList.Add( SUNDAYONTIME__DigitalInput__ + i, SUNDAYONTIME[i+1] );
    }
    
    SUNDAYOFFTIME = new InOutArray<DigitalInput>( 24, this );
    for( uint i = 0; i < 24; i++ )
    {
        SUNDAYOFFTIME[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( SUNDAYOFFTIME__DigitalInput__ + i, SUNDAYOFFTIME__DigitalInput__, this );
        m_DigitalInputList.Add( SUNDAYOFFTIME__DigitalInput__ + i, SUNDAYOFFTIME[i+1] );
    }
    
    MONDAYONFB = new Crestron.Logos.SplusObjects.DigitalOutput( MONDAYONFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( MONDAYONFB__DigitalOutput__, MONDAYONFB );
    
    TUESDAYONFB = new Crestron.Logos.SplusObjects.DigitalOutput( TUESDAYONFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( TUESDAYONFB__DigitalOutput__, TUESDAYONFB );
    
    WEDNESDAYONFB = new Crestron.Logos.SplusObjects.DigitalOutput( WEDNESDAYONFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( WEDNESDAYONFB__DigitalOutput__, WEDNESDAYONFB );
    
    THURSDAYONFB = new Crestron.Logos.SplusObjects.DigitalOutput( THURSDAYONFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( THURSDAYONFB__DigitalOutput__, THURSDAYONFB );
    
    FRIDAYONFB = new Crestron.Logos.SplusObjects.DigitalOutput( FRIDAYONFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( FRIDAYONFB__DigitalOutput__, FRIDAYONFB );
    
    SATURDAYONFB = new Crestron.Logos.SplusObjects.DigitalOutput( SATURDAYONFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( SATURDAYONFB__DigitalOutput__, SATURDAYONFB );
    
    SUNDAYONFB = new Crestron.Logos.SplusObjects.DigitalOutput( SUNDAYONFB__DigitalOutput__, this );
    m_DigitalOutputList.Add( SUNDAYONFB__DigitalOutput__, SUNDAYONFB );
    
    SCREENSON = new Crestron.Logos.SplusObjects.DigitalOutput( SCREENSON__DigitalOutput__, this );
    m_DigitalOutputList.Add( SCREENSON__DigitalOutput__, SCREENSON );
    
    SCREENSOFF = new Crestron.Logos.SplusObjects.DigitalOutput( SCREENSOFF__DigitalOutput__, this );
    m_DigitalOutputList.Add( SCREENSOFF__DigitalOutput__, SCREENSOFF );
    
    MONDAYONTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( MONDAYONTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( MONDAYONTIMEFB__AnalogSerialOutput__, MONDAYONTIMEFB );
    
    MONDAYOFFTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( MONDAYOFFTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( MONDAYOFFTIMEFB__AnalogSerialOutput__, MONDAYOFFTIMEFB );
    
    TUESDAYONTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( TUESDAYONTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( TUESDAYONTIMEFB__AnalogSerialOutput__, TUESDAYONTIMEFB );
    
    TUESDAYOFFTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( TUESDAYOFFTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( TUESDAYOFFTIMEFB__AnalogSerialOutput__, TUESDAYOFFTIMEFB );
    
    WEDNESDAYONTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( WEDNESDAYONTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( WEDNESDAYONTIMEFB__AnalogSerialOutput__, WEDNESDAYONTIMEFB );
    
    WEDNESDAYOFFTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( WEDNESDAYOFFTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( WEDNESDAYOFFTIMEFB__AnalogSerialOutput__, WEDNESDAYOFFTIMEFB );
    
    THURSDAYONTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( THURSDAYONTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( THURSDAYONTIMEFB__AnalogSerialOutput__, THURSDAYONTIMEFB );
    
    THURSDAYOFFTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( THURSDAYOFFTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( THURSDAYOFFTIMEFB__AnalogSerialOutput__, THURSDAYOFFTIMEFB );
    
    FRIDAYONTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( FRIDAYONTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( FRIDAYONTIMEFB__AnalogSerialOutput__, FRIDAYONTIMEFB );
    
    FRIDAYOFFTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( FRIDAYOFFTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( FRIDAYOFFTIMEFB__AnalogSerialOutput__, FRIDAYOFFTIMEFB );
    
    SATURDAYONTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( SATURDAYONTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( SATURDAYONTIMEFB__AnalogSerialOutput__, SATURDAYONTIMEFB );
    
    SATURDAYOFFTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( SATURDAYOFFTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( SATURDAYOFFTIMEFB__AnalogSerialOutput__, SATURDAYOFFTIMEFB );
    
    SUNDAYONTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( SUNDAYONTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( SUNDAYONTIMEFB__AnalogSerialOutput__, SUNDAYONTIMEFB );
    
    SUNDAYOFFTIMEFB = new Crestron.Logos.SplusObjects.StringOutput( SUNDAYOFFTIMEFB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( SUNDAYOFFTIMEFB__AnalogSerialOutput__, SUNDAYOFFTIMEFB );
    
    FILELOCATION = new StringParameter( FILELOCATION__Parameter__, this );
    m_ParameterList.Add( FILELOCATION__Parameter__, FILELOCATION );
    
    
    INITIALIZE.OnDigitalPush.Add( new InputChangeHandlerWrapper( INITIALIZE_OnPush_0, false ) );
    HOURLYCHECK.OnDigitalPush.Add( new InputChangeHandlerWrapper( HOURLYCHECK_OnPush_1, false ) );
    MONDAYONOFF.OnDigitalPush.Add( new InputChangeHandlerWrapper( MONDAYONOFF_OnPush_2, false ) );
    TUESDAYONOFF.OnDigitalPush.Add( new InputChangeHandlerWrapper( TUESDAYONOFF_OnPush_3, false ) );
    WEDNESDAYONOFF.OnDigitalPush.Add( new InputChangeHandlerWrapper( WEDNESDAYONOFF_OnPush_4, false ) );
    THURSDAYONOFF.OnDigitalPush.Add( new InputChangeHandlerWrapper( THURSDAYONOFF_OnPush_5, false ) );
    FRIDAYONOFF.OnDigitalPush.Add( new InputChangeHandlerWrapper( FRIDAYONOFF_OnPush_6, false ) );
    SATURDAYONOFF.OnDigitalPush.Add( new InputChangeHandlerWrapper( SATURDAYONOFF_OnPush_7, false ) );
    SUNDAYONOFF.OnDigitalPush.Add( new InputChangeHandlerWrapper( SUNDAYONOFF_OnPush_8, false ) );
    for( uint i = 0; i < 24; i++ )
        MONDAYONTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( MONDAYONTIME_OnPush_9, false ) );
        
    for( uint i = 0; i < 24; i++ )
        MONDAYOFFTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( MONDAYOFFTIME_OnPush_10, false ) );
        
    for( uint i = 0; i < 24; i++ )
        TUESDAYONTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( TUESDAYONTIME_OnPush_11, false ) );
        
    for( uint i = 0; i < 24; i++ )
        TUESDAYOFFTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( TUESDAYOFFTIME_OnPush_12, false ) );
        
    for( uint i = 0; i < 24; i++ )
        WEDNESDAYONTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( WEDNESDAYONTIME_OnPush_13, false ) );
        
    for( uint i = 0; i < 24; i++ )
        WEDNESDAYOFFTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( WEDNESDAYOFFTIME_OnPush_14, false ) );
        
    for( uint i = 0; i < 24; i++ )
        THURSDAYONTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( THURSDAYONTIME_OnPush_15, false ) );
        
    for( uint i = 0; i < 24; i++ )
        THURSDAYOFFTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( THURSDAYOFFTIME_OnPush_16, false ) );
        
    for( uint i = 0; i < 24; i++ )
        FRIDAYONTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( FRIDAYONTIME_OnPush_17, false ) );
        
    for( uint i = 0; i < 24; i++ )
        FRIDAYOFFTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( FRIDAYOFFTIME_OnPush_18, false ) );
        
    for( uint i = 0; i < 24; i++ )
        SATURDAYONTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( SATURDAYONTIME_OnPush_19, false ) );
        
    for( uint i = 0; i < 24; i++ )
        SATURDAYOFFTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( SATURDAYOFFTIME_OnPush_20, false ) );
        
    for( uint i = 0; i < 24; i++ )
        SUNDAYONTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( SUNDAYONTIME_OnPush_21, false ) );
        
    for( uint i = 0; i < 24; i++ )
        SUNDAYOFFTIME[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( SUNDAYOFFTIME_OnPush_22, false ) );
        
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SCHEDULER___BARCLAYS____MR ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint FILELOCATION__Parameter__ = 10;
const uint INITIALIZE__DigitalInput__ = 0;
const uint HOURLYCHECK__DigitalInput__ = 1;
const uint MONDAYONOFF__DigitalInput__ = 2;
const uint TUESDAYONOFF__DigitalInput__ = 3;
const uint WEDNESDAYONOFF__DigitalInput__ = 4;
const uint THURSDAYONOFF__DigitalInput__ = 5;
const uint FRIDAYONOFF__DigitalInput__ = 6;
const uint SATURDAYONOFF__DigitalInput__ = 7;
const uint SUNDAYONOFF__DigitalInput__ = 8;
const uint MONDAYONTIME__DigitalInput__ = 9;
const uint MONDAYOFFTIME__DigitalInput__ = 33;
const uint TUESDAYONTIME__DigitalInput__ = 57;
const uint TUESDAYOFFTIME__DigitalInput__ = 81;
const uint WEDNESDAYONTIME__DigitalInput__ = 105;
const uint WEDNESDAYOFFTIME__DigitalInput__ = 129;
const uint THURSDAYONTIME__DigitalInput__ = 153;
const uint THURSDAYOFFTIME__DigitalInput__ = 177;
const uint FRIDAYONTIME__DigitalInput__ = 201;
const uint FRIDAYOFFTIME__DigitalInput__ = 225;
const uint SATURDAYONTIME__DigitalInput__ = 249;
const uint SATURDAYOFFTIME__DigitalInput__ = 273;
const uint SUNDAYONTIME__DigitalInput__ = 297;
const uint SUNDAYOFFTIME__DigitalInput__ = 321;
const uint MONDAYONFB__DigitalOutput__ = 0;
const uint TUESDAYONFB__DigitalOutput__ = 1;
const uint WEDNESDAYONFB__DigitalOutput__ = 2;
const uint THURSDAYONFB__DigitalOutput__ = 3;
const uint FRIDAYONFB__DigitalOutput__ = 4;
const uint SATURDAYONFB__DigitalOutput__ = 5;
const uint SUNDAYONFB__DigitalOutput__ = 6;
const uint SCREENSON__DigitalOutput__ = 7;
const uint SCREENSOFF__DigitalOutput__ = 8;
const uint MONDAYONTIMEFB__AnalogSerialOutput__ = 0;
const uint MONDAYOFFTIMEFB__AnalogSerialOutput__ = 1;
const uint TUESDAYONTIMEFB__AnalogSerialOutput__ = 2;
const uint TUESDAYOFFTIMEFB__AnalogSerialOutput__ = 3;
const uint WEDNESDAYONTIMEFB__AnalogSerialOutput__ = 4;
const uint WEDNESDAYOFFTIMEFB__AnalogSerialOutput__ = 5;
const uint THURSDAYONTIMEFB__AnalogSerialOutput__ = 6;
const uint THURSDAYOFFTIMEFB__AnalogSerialOutput__ = 7;
const uint FRIDAYONTIMEFB__AnalogSerialOutput__ = 8;
const uint FRIDAYOFFTIMEFB__AnalogSerialOutput__ = 9;
const uint SATURDAYONTIMEFB__AnalogSerialOutput__ = 10;
const uint SATURDAYOFFTIMEFB__AnalogSerialOutput__ = 11;
const uint SUNDAYONTIMEFB__AnalogSerialOutput__ = 12;
const uint SUNDAYOFFTIMEFB__AnalogSerialOutput__ = 13;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    [SplusStructAttribute(0, false, true)]
            public CrestronString [] DAYS;
            [SplusStructAttribute(1, false, true)]
            public CrestronString [] ONTIME;
            [SplusStructAttribute(2, false, true)]
            public CrestronString [] OFFTIME;
            [SplusStructAttribute(3, false, true)]
            public ushort MONDAYSTATE = 0;
            [SplusStructAttribute(4, false, true)]
            public ushort TUESDAYSTATE = 0;
            [SplusStructAttribute(5, false, true)]
            public ushort WEDNESDAYSTATE = 0;
            [SplusStructAttribute(6, false, true)]
            public ushort THURSDAYSTATE = 0;
            [SplusStructAttribute(7, false, true)]
            public ushort FRIDAYSTATE = 0;
            [SplusStructAttribute(8, false, true)]
            public ushort SATURDAYSTATE = 0;
            [SplusStructAttribute(9, false, true)]
            public ushort SUNDAYSTATE = 0;
            [SplusStructAttribute(10, false, true)]
            public ushort [] DAYSTATE;
            
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
