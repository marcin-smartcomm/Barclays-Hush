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

namespace UserModule_PIN_HANDLER_FORC_
{
    public class UserModuleClass_PIN_HANDLER_FORC_ : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        Crestron.Logos.SplusObjects.DigitalInput INITIALIZE;
        Crestron.Logos.SplusObjects.DigitalInput BACKSPACE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> NUM;
        Crestron.Logos.SplusObjects.DigitalOutput BACKSPACEVIS;
        Crestron.Logos.SplusObjects.DigitalOutput CORRECTPIN;
        Crestron.Logos.SplusObjects.DigitalOutput WRONGPIN;
        Crestron.Logos.SplusObjects.StringOutput PINSTARS;
        Crestron.Logos.SplusObjects.StringOutput CURRENTUSERPIN;
        Crestron.Logos.SplusObjects.StringOutput SENDPINFORCHECK;
        Crestron.Logos.SplusObjects.StringInput MAIN_PROGRAM_RX__DOLLAR__;
        CrestronString USERPIN;
        CrestronString SBUF;
        CrestronString PIN;
        ushort PINLENGTH = 0;
        ushort X = 0;
        short NFILEHANDLE = 0;
        private void RESETVALUES (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 22;
            PINLENGTH = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 23;
            PIN  .UpdateValue ( ""  ) ; 
            __context__.SourceCodeLine = 24;
            PINSTARS  .UpdateValue ( ""  ) ; 
            
            }
            
        private void UPDATESTARS (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 29;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 0))  ) ) 
                {
                __context__.SourceCodeLine = 30;
                PINSTARS  .UpdateValue ( ""  ) ; 
                }
            
            __context__.SourceCodeLine = 31;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 1))  ) ) 
                {
                __context__.SourceCodeLine = 32;
                PINSTARS  .UpdateValue ( "*"  ) ; 
                }
            
            __context__.SourceCodeLine = 33;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 2))  ) ) 
                {
                __context__.SourceCodeLine = 34;
                PINSTARS  .UpdateValue ( "**"  ) ; 
                }
            
            __context__.SourceCodeLine = 35;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 3))  ) ) 
                {
                __context__.SourceCodeLine = 36;
                PINSTARS  .UpdateValue ( "***"  ) ; 
                }
            
            
            }
            
        private void CHECKBACKSPACEVIS (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 41;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( PINLENGTH > 0 ))  ) ) 
                {
                __context__.SourceCodeLine = 42;
                BACKSPACEVIS  .Value = (ushort) ( 1 ) ; 
                }
            
            __context__.SourceCodeLine = 44;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 0))  ) ) 
                {
                __context__.SourceCodeLine = 45;
                BACKSPACEVIS  .Value = (ushort) ( 0 ) ; 
                }
            
            
            }
            
        object INITIALIZE_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 50;
                RESETVALUES (  __context__  ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object NUM_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 55;
            X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
            __context__.SourceCodeLine = 57;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (X == 10))  ) ) 
                {
                __context__.SourceCodeLine = 58;
                X = (ushort) ( 0 ) ; 
                }
            
            __context__.SourceCodeLine = 60;
            PIN  .UpdateValue ( PIN + Functions.ItoA (  (int) ( X ) )  ) ; 
            __context__.SourceCodeLine = 62;
            PINLENGTH = (ushort) ( (PINLENGTH + 1) ) ; 
            __context__.SourceCodeLine = 64;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( PINLENGTH > 3 ))  ) ) 
                { 
                __context__.SourceCodeLine = 66;
                SENDPINFORCHECK  .UpdateValue ( "Login:" + PIN  ) ; 
                } 
            
            __context__.SourceCodeLine = 69;
            UPDATESTARS (  __context__  ) ; 
            __context__.SourceCodeLine = 70;
            CHECKBACKSPACEVIS (  __context__  ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object BACKSPACE_OnPush_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 75;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 0))  ) ) 
            { 
            } 
        
        __context__.SourceCodeLine = 77;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 79;
            RESETVALUES (  __context__  ) ; 
            } 
        
        __context__.SourceCodeLine = 82;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 2))  ) ) 
            { 
            __context__.SourceCodeLine = 84;
            PIN  .UpdateValue ( Functions.Left ( PIN ,  (int) ( 1 ) )  ) ; 
            __context__.SourceCodeLine = 85;
            PINLENGTH = (ushort) ( 1 ) ; 
            } 
        
        __context__.SourceCodeLine = 88;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 3))  ) ) 
            { 
            __context__.SourceCodeLine = 90;
            PIN  .UpdateValue ( Functions.Left ( PIN ,  (int) ( 2 ) )  ) ; 
            __context__.SourceCodeLine = 91;
            PINLENGTH = (ushort) ( 2 ) ; 
            } 
        
        __context__.SourceCodeLine = 94;
        UPDATESTARS (  __context__  ) ; 
        __context__.SourceCodeLine = 95;
        CHECKBACKSPACEVIS (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object MAIN_PROGRAM_RX__DOLLAR___OnChange_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 100;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (MAIN_PROGRAM_RX__DOLLAR__ == "Login:Success"))  ) ) 
            { 
            __context__.SourceCodeLine = 102;
            Functions.Pulse ( 1, CORRECTPIN ) ; 
            __context__.SourceCodeLine = 103;
            CURRENTUSERPIN  .UpdateValue ( PIN  ) ; 
            __context__.SourceCodeLine = 104;
            RESETVALUES (  __context__  ) ; 
            } 
        
        __context__.SourceCodeLine = 106;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (MAIN_PROGRAM_RX__DOLLAR__ == "Login:Failed"))  ) ) 
            { 
            __context__.SourceCodeLine = 108;
            Functions.Pulse ( 1, WRONGPIN ) ; 
            __context__.SourceCodeLine = 109;
            RESETVALUES (  __context__  ) ; 
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}


public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    USERPIN  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
    SBUF  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
    PIN  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 5, this );
    
    INITIALIZE = new Crestron.Logos.SplusObjects.DigitalInput( INITIALIZE__DigitalInput__, this );
    m_DigitalInputList.Add( INITIALIZE__DigitalInput__, INITIALIZE );
    
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
    
    CORRECTPIN = new Crestron.Logos.SplusObjects.DigitalOutput( CORRECTPIN__DigitalOutput__, this );
    m_DigitalOutputList.Add( CORRECTPIN__DigitalOutput__, CORRECTPIN );
    
    WRONGPIN = new Crestron.Logos.SplusObjects.DigitalOutput( WRONGPIN__DigitalOutput__, this );
    m_DigitalOutputList.Add( WRONGPIN__DigitalOutput__, WRONGPIN );
    
    MAIN_PROGRAM_RX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( MAIN_PROGRAM_RX__DOLLAR____AnalogSerialInput__, 500, this );
    m_StringInputList.Add( MAIN_PROGRAM_RX__DOLLAR____AnalogSerialInput__, MAIN_PROGRAM_RX__DOLLAR__ );
    
    PINSTARS = new Crestron.Logos.SplusObjects.StringOutput( PINSTARS__AnalogSerialOutput__, this );
    m_StringOutputList.Add( PINSTARS__AnalogSerialOutput__, PINSTARS );
    
    CURRENTUSERPIN = new Crestron.Logos.SplusObjects.StringOutput( CURRENTUSERPIN__AnalogSerialOutput__, this );
    m_StringOutputList.Add( CURRENTUSERPIN__AnalogSerialOutput__, CURRENTUSERPIN );
    
    SENDPINFORCHECK = new Crestron.Logos.SplusObjects.StringOutput( SENDPINFORCHECK__AnalogSerialOutput__, this );
    m_StringOutputList.Add( SENDPINFORCHECK__AnalogSerialOutput__, SENDPINFORCHECK );
    
    
    INITIALIZE.OnDigitalPush.Add( new InputChangeHandlerWrapper( INITIALIZE_OnPush_0, false ) );
    for( uint i = 0; i < 10; i++ )
        NUM[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_OnPush_1, false ) );
        
    BACKSPACE.OnDigitalPush.Add( new InputChangeHandlerWrapper( BACKSPACE_OnPush_2, false ) );
    MAIN_PROGRAM_RX__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( MAIN_PROGRAM_RX__DOLLAR___OnChange_3, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_PIN_HANDLER_FORC_ ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint INITIALIZE__DigitalInput__ = 0;
const uint BACKSPACE__DigitalInput__ = 1;
const uint NUM__DigitalInput__ = 2;
const uint BACKSPACEVIS__DigitalOutput__ = 0;
const uint CORRECTPIN__DigitalOutput__ = 1;
const uint WRONGPIN__DigitalOutput__ = 2;
const uint PINSTARS__AnalogSerialOutput__ = 0;
const uint CURRENTUSERPIN__AnalogSerialOutput__ = 1;
const uint SENDPINFORCHECK__AnalogSerialOutput__ = 2;
const uint MAIN_PROGRAM_RX__DOLLAR____AnalogSerialInput__ = 0;

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
