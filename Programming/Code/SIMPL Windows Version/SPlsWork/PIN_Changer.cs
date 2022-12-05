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

namespace UserModule_PIN_CHANGER
{
    public class UserModuleClass_PIN_CHANGER : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        Crestron.Logos.SplusObjects.DigitalInput RESETCHANGEPINVALUES;
        Crestron.Logos.SplusObjects.DigitalInput BACKSPACE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> NUM;
        Crestron.Logos.SplusObjects.StringInput USERPIN;
        Crestron.Logos.SplusObjects.DigitalOutput BACKSPACEVIS;
        Crestron.Logos.SplusObjects.DigitalOutput CURRENTPINCORRECT;
        Crestron.Logos.SplusObjects.DigitalOutput CURRENTPINWRONG;
        Crestron.Logos.SplusObjects.DigitalOutput NEWPINCORRECT;
        Crestron.Logos.SplusObjects.DigitalOutput NEWPINCONFIRMCORRECT;
        Crestron.Logos.SplusObjects.DigitalOutput NEWPINCONFIRMWRONG;
        Crestron.Logos.SplusObjects.DigitalOutput UPDATE_PIN_HANDLER;
        Crestron.Logos.SplusObjects.StringOutput PINSTARS;
        CrestronString PIN;
        CrestronString CURRENTPIN;
        CrestronString NEWPIN;
        ushort PINLENGTH = 0;
        ushort X = 0;
        ushort CURRENTPINENTERED = 0;
        ushort NEWPINENTERED = 0;
        ushort NEWPINCONFIRMENTERED = 0;
        short NFILEHANDLE = 0;
        private void RESETVALUES (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 25;
            PINLENGTH = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 26;
            PIN  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 27;
            PINSTARS  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 28;
            CURRENTPINENTERED = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 29;
            NEWPINENTERED = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 30;
            NEWPINCONFIRMENTERED = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 31;
            CURRENTPINCORRECT  .Value = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 32;
            CURRENTPINWRONG  .Value = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 33;
            NEWPINCORRECT  .Value = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 34;
            NEWPINCONFIRMCORRECT  .Value = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 35;
            NEWPINCONFIRMWRONG  .Value = (ushort) ( 0 ) ; 
            
            }
            
        private void CLEARPINENTRYVARIABLES (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 40;
            PINLENGTH = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 41;
            PIN  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 42;
            PINSTARS  .UpdateValue ( ""  ) ; 
            
            }
            
        private void INITIALIZEVALUES (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 47;
            RESETVALUES (  __context__  ) ; 
            
            }
            
        private void UPDATESTARS (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 52;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 0))  ) ) 
                {
                __context__.SourceCodeLine = 53;
                PINSTARS  .UpdateValue ( ""  ) ; 
                }
            
            __context__.SourceCodeLine = 54;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 1))  ) ) 
                {
                __context__.SourceCodeLine = 55;
                PINSTARS  .UpdateValue ( "*"  ) ; 
                }
            
            __context__.SourceCodeLine = 56;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 2))  ) ) 
                {
                __context__.SourceCodeLine = 57;
                PINSTARS  .UpdateValue ( "**"  ) ; 
                }
            
            __context__.SourceCodeLine = 58;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 3))  ) ) 
                {
                __context__.SourceCodeLine = 59;
                PINSTARS  .UpdateValue ( "***"  ) ; 
                }
            
            
            }
            
        private void CHECKBACKSPACEVIS (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 64;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( PINLENGTH > 0 ))  ) ) 
                {
                __context__.SourceCodeLine = 65;
                BACKSPACEVIS  .Value = (ushort) ( 1 ) ; 
                }
            
            __context__.SourceCodeLine = 67;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 0))  ) ) 
                {
                __context__.SourceCodeLine = 68;
                BACKSPACEVIS  .Value = (ushort) ( 0 ) ; 
                }
            
            
            }
            
        private void SAVENEWPININFILE (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 73;
            StartFileOperations ( ) ; 
            __context__.SourceCodeLine = 75;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (FileDelete( "\\NVRAM\\PIN.txt" ) != 0))  ) ) 
                {
                __context__.SourceCodeLine = 77;
                Print( "Error deleting file\r\n") ; 
                }
            
            __context__.SourceCodeLine = 79;
            NFILEHANDLE = (short) ( FileOpen( "\\NVRAM\\PIN.txt" ,(ushort) ((256 | 1) | 16384) ) ) ; 
            __context__.SourceCodeLine = 80;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( NFILEHANDLE >= 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 82;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( FileWrite( (short)( NFILEHANDLE ) , NEWPIN , (ushort)( 4096 ) ) > 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 83;
                    Print( "Written to file:\r\n{0}", NEWPIN ) ; 
                    }
                
                __context__.SourceCodeLine = 84;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (FileClose( (short)( NFILEHANDLE ) ) != 0))  ) ) 
                    {
                    __context__.SourceCodeLine = 85;
                    Print( "Error closing file \r\n") ; 
                    }
                
                } 
            
            __context__.SourceCodeLine = 88;
            EndFileOperations ( ) ; 
            
            }
            
        private void EVALUATEPINENTERED (  SplusExecutionContext __context__, CrestronString PINTOCHECK ) 
            { 
            
            __context__.SourceCodeLine = 93;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (CURRENTPINENTERED == 0))  ) ) 
                { 
                __context__.SourceCodeLine = 95;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINTOCHECK == CURRENTPIN))  ) ) 
                    { 
                    __context__.SourceCodeLine = 97;
                    CURRENTPINENTERED = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 98;
                    CURRENTPINCORRECT  .Value = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 99;
                    CLEARPINENTRYVARIABLES (  __context__  ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 103;
                    Functions.Pulse ( 1, CURRENTPINWRONG ) ; 
                    __context__.SourceCodeLine = 104;
                    RESETVALUES (  __context__  ) ; 
                    } 
                
                } 
            
            else 
                {
                __context__.SourceCodeLine = 107;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (CURRENTPINENTERED == 1) ) && Functions.TestForTrue ( Functions.BoolToInt (NEWPINENTERED == 0) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 109;
                    NEWPIN  .UpdateValue ( PINTOCHECK  ) ; 
                    __context__.SourceCodeLine = 110;
                    NEWPINENTERED = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 111;
                    NEWPINCORRECT  .Value = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 112;
                    CLEARPINENTRYVARIABLES (  __context__  ) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 114;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (NEWPINENTERED == 1) ) && Functions.TestForTrue ( Functions.BoolToInt (NEWPINCONFIRMENTERED == 0) )) ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 116;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINTOCHECK == NEWPIN))  ) ) 
                            { 
                            __context__.SourceCodeLine = 118;
                            NEWPINCONFIRMCORRECT  .Value = (ushort) ( 1 ) ; 
                            __context__.SourceCodeLine = 119;
                            SAVENEWPININFILE (  __context__  ) ; 
                            __context__.SourceCodeLine = 120;
                            Functions.Delay (  (int) ( 100 ) ) ; 
                            __context__.SourceCodeLine = 121;
                            RESETVALUES (  __context__  ) ; 
                            __context__.SourceCodeLine = 122;
                            Functions.Pulse ( 1, UPDATE_PIN_HANDLER ) ; 
                            } 
                        
                        else 
                            { 
                            __context__.SourceCodeLine = 126;
                            Functions.Pulse ( 1, NEWPINCONFIRMWRONG ) ; 
                            __context__.SourceCodeLine = 127;
                            CLEARPINENTRYVARIABLES (  __context__  ) ; 
                            } 
                        
                        } 
                    
                    }
                
                }
            
            
            }
            
        object USERPIN_OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 134;
                CURRENTPIN  .UpdateValue ( USERPIN  ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object RESETCHANGEPINVALUES_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 139;
            RESETVALUES (  __context__  ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object NUM_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 144;
        X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
        __context__.SourceCodeLine = 146;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (X == 10))  ) ) 
            {
            __context__.SourceCodeLine = 147;
            X = (ushort) ( 0 ) ; 
            }
        
        __context__.SourceCodeLine = 149;
        PIN  .UpdateValue ( PIN + Functions.ItoA (  (int) ( X ) )  ) ; 
        __context__.SourceCodeLine = 151;
        PINLENGTH = (ushort) ( (PINLENGTH + 1) ) ; 
        __context__.SourceCodeLine = 153;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( PINLENGTH > 3 ))  ) ) 
            { 
            __context__.SourceCodeLine = 155;
            EVALUATEPINENTERED (  __context__ , PIN) ; 
            } 
        
        __context__.SourceCodeLine = 158;
        UPDATESTARS (  __context__  ) ; 
        __context__.SourceCodeLine = 159;
        CHECKBACKSPACEVIS (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object BACKSPACE_OnPush_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 164;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 0))  ) ) 
            { 
            } 
        
        __context__.SourceCodeLine = 166;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 168;
            CLEARPINENTRYVARIABLES (  __context__  ) ; 
            } 
        
        __context__.SourceCodeLine = 171;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 2))  ) ) 
            { 
            __context__.SourceCodeLine = 173;
            PIN  .UpdateValue ( Functions.Left ( PIN ,  (int) ( 1 ) )  ) ; 
            __context__.SourceCodeLine = 174;
            PINLENGTH = (ushort) ( 1 ) ; 
            } 
        
        __context__.SourceCodeLine = 177;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 3))  ) ) 
            { 
            __context__.SourceCodeLine = 179;
            PIN  .UpdateValue ( Functions.Left ( PIN ,  (int) ( 2 ) )  ) ; 
            __context__.SourceCodeLine = 180;
            PINLENGTH = (ushort) ( 2 ) ; 
            } 
        
        __context__.SourceCodeLine = 183;
        UPDATESTARS (  __context__  ) ; 
        __context__.SourceCodeLine = 184;
        CHECKBACKSPACEVIS (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}


public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    PIN  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
    CURRENTPIN  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
    NEWPIN  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
    
    RESETCHANGEPINVALUES = new Crestron.Logos.SplusObjects.DigitalInput( RESETCHANGEPINVALUES__DigitalInput__, this );
    m_DigitalInputList.Add( RESETCHANGEPINVALUES__DigitalInput__, RESETCHANGEPINVALUES );
    
    BACKSPACE = new Crestron.Logos.SplusObjects.DigitalInput( BACKSPACE__DigitalInput__, this );
    m_DigitalInputList.Add( BACKSPACE__DigitalInput__, BACKSPACE );
    
    NUM = new InOutArray<DigitalInput>( 10, this );
    for( uint i = 0; i < 10; i++ )
    {
        NUM[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( NUM__DigitalInput__ + i, NUM__DigitalInput__, this );
        m_DigitalInputList.Add( NUM__DigitalInput__ + i, NUM[i+1] );
    }
    
    BACKSPACEVIS = new Crestron.Logos.SplusObjects.DigitalOutput( BACKSPACEVIS__DigitalOutput__, this );
    m_DigitalOutputList.Add( BACKSPACEVIS__DigitalOutput__, BACKSPACEVIS );
    
    CURRENTPINCORRECT = new Crestron.Logos.SplusObjects.DigitalOutput( CURRENTPINCORRECT__DigitalOutput__, this );
    m_DigitalOutputList.Add( CURRENTPINCORRECT__DigitalOutput__, CURRENTPINCORRECT );
    
    CURRENTPINWRONG = new Crestron.Logos.SplusObjects.DigitalOutput( CURRENTPINWRONG__DigitalOutput__, this );
    m_DigitalOutputList.Add( CURRENTPINWRONG__DigitalOutput__, CURRENTPINWRONG );
    
    NEWPINCORRECT = new Crestron.Logos.SplusObjects.DigitalOutput( NEWPINCORRECT__DigitalOutput__, this );
    m_DigitalOutputList.Add( NEWPINCORRECT__DigitalOutput__, NEWPINCORRECT );
    
    NEWPINCONFIRMCORRECT = new Crestron.Logos.SplusObjects.DigitalOutput( NEWPINCONFIRMCORRECT__DigitalOutput__, this );
    m_DigitalOutputList.Add( NEWPINCONFIRMCORRECT__DigitalOutput__, NEWPINCONFIRMCORRECT );
    
    NEWPINCONFIRMWRONG = new Crestron.Logos.SplusObjects.DigitalOutput( NEWPINCONFIRMWRONG__DigitalOutput__, this );
    m_DigitalOutputList.Add( NEWPINCONFIRMWRONG__DigitalOutput__, NEWPINCONFIRMWRONG );
    
    UPDATE_PIN_HANDLER = new Crestron.Logos.SplusObjects.DigitalOutput( UPDATE_PIN_HANDLER__DigitalOutput__, this );
    m_DigitalOutputList.Add( UPDATE_PIN_HANDLER__DigitalOutput__, UPDATE_PIN_HANDLER );
    
    USERPIN = new Crestron.Logos.SplusObjects.StringInput( USERPIN__AnalogSerialInput__, 5, this );
    m_StringInputList.Add( USERPIN__AnalogSerialInput__, USERPIN );
    
    PINSTARS = new Crestron.Logos.SplusObjects.StringOutput( PINSTARS__AnalogSerialOutput__, this );
    m_StringOutputList.Add( PINSTARS__AnalogSerialOutput__, PINSTARS );
    
    
    USERPIN.OnSerialChange.Add( new InputChangeHandlerWrapper( USERPIN_OnChange_0, false ) );
    RESETCHANGEPINVALUES.OnDigitalPush.Add( new InputChangeHandlerWrapper( RESETCHANGEPINVALUES_OnPush_1, false ) );
    for( uint i = 0; i < 10; i++ )
        NUM[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_OnPush_2, false ) );
        
    BACKSPACE.OnDigitalPush.Add( new InputChangeHandlerWrapper( BACKSPACE_OnPush_3, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_PIN_CHANGER ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint RESETCHANGEPINVALUES__DigitalInput__ = 0;
const uint BACKSPACE__DigitalInput__ = 1;
const uint NUM__DigitalInput__ = 2;
const uint USERPIN__AnalogSerialInput__ = 0;
const uint BACKSPACEVIS__DigitalOutput__ = 0;
const uint CURRENTPINCORRECT__DigitalOutput__ = 1;
const uint CURRENTPINWRONG__DigitalOutput__ = 2;
const uint NEWPINCORRECT__DigitalOutput__ = 3;
const uint NEWPINCONFIRMCORRECT__DigitalOutput__ = 4;
const uint NEWPINCONFIRMWRONG__DigitalOutput__ = 5;
const uint UPDATE_PIN_HANDLER__DigitalOutput__ = 6;
const uint PINSTARS__AnalogSerialOutput__ = 0;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
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
