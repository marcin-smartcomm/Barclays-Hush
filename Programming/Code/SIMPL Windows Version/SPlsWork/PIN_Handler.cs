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

namespace UserModule_PIN_HANDLER
{
    public class UserModuleClass_PIN_HANDLER : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        StringParameter MASTER_PIN;
        StringParameter USERPINFILELOCATION;
        Crestron.Logos.SplusObjects.DigitalInput INITIALIZE;
        Crestron.Logos.SplusObjects.DigitalInput BACKSPACE;
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> NUM;
        Crestron.Logos.SplusObjects.DigitalOutput BACKSPACEVIS;
        Crestron.Logos.SplusObjects.DigitalOutput CORRECTPIN;
        Crestron.Logos.SplusObjects.DigitalOutput WRONGPIN;
        Crestron.Logos.SplusObjects.StringOutput PINSTARS;
        Crestron.Logos.SplusObjects.StringOutput CURRENTUSERPIN;
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
            
        private void INITIALIZEVALUES (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 29;
            RESETVALUES (  __context__  ) ; 
            __context__.SourceCodeLine = 31;
            StartFileOperations ( ) ; 
            __context__.SourceCodeLine = 32;
            NFILEHANDLE = (short) ( FileOpen( USERPINFILELOCATION  ,(ushort) (0 | 16384) ) ) ; 
            __context__.SourceCodeLine = 33;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( NFILEHANDLE >= 0 ))  ) ) 
                { 
                __context__.SourceCodeLine = 35;
                while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( FileRead( (short)( NFILEHANDLE ) , SBUF , (ushort)( 10 ) ) > 0 ))  ) ) 
                    {
                    __context__.SourceCodeLine = 36;
                    USERPIN  .UpdateValue ( SBUF  ) ; 
                    __context__.SourceCodeLine = 35;
                    }
                
                __context__.SourceCodeLine = 37;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (FileClose( (short)( NFILEHANDLE ) ) != 0))  ) ) 
                    {
                    __context__.SourceCodeLine = 38;
                    Print( "Error closing file\r\n") ; 
                    }
                
                } 
            
            __context__.SourceCodeLine = 40;
            EndFileOperations ( ) ; 
            __context__.SourceCodeLine = 42;
            CURRENTUSERPIN  .UpdateValue ( USERPIN  ) ; 
            
            }
            
        private void EVALUATEPINENTERED (  SplusExecutionContext __context__, CrestronString PINENTERED ) 
            { 
            
            __context__.SourceCodeLine = 47;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (PINENTERED == USERPIN) ) || Functions.TestForTrue ( Functions.BoolToInt (PINENTERED == MASTER_PIN ) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 49;
                Functions.Pulse ( 1, CORRECTPIN ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 53;
                Functions.Pulse ( 1, WRONGPIN ) ; 
                } 
            
            
            }
            
        private void UPDATESTARS (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 59;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 0))  ) ) 
                {
                __context__.SourceCodeLine = 60;
                PINSTARS  .UpdateValue ( ""  ) ; 
                }
            
            __context__.SourceCodeLine = 61;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 1))  ) ) 
                {
                __context__.SourceCodeLine = 62;
                PINSTARS  .UpdateValue ( "*"  ) ; 
                }
            
            __context__.SourceCodeLine = 63;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 2))  ) ) 
                {
                __context__.SourceCodeLine = 64;
                PINSTARS  .UpdateValue ( "**"  ) ; 
                }
            
            __context__.SourceCodeLine = 65;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 3))  ) ) 
                {
                __context__.SourceCodeLine = 66;
                PINSTARS  .UpdateValue ( "***"  ) ; 
                }
            
            
            }
            
        private void CHECKBACKSPACEVIS (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 71;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( PINLENGTH > 0 ))  ) ) 
                {
                __context__.SourceCodeLine = 72;
                BACKSPACEVIS  .Value = (ushort) ( 1 ) ; 
                }
            
            __context__.SourceCodeLine = 74;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 0))  ) ) 
                {
                __context__.SourceCodeLine = 75;
                BACKSPACEVIS  .Value = (ushort) ( 0 ) ; 
                }
            
            
            }
            
        object INITIALIZE_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 80;
                INITIALIZEVALUES (  __context__  ) ; 
                
                
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
            
            __context__.SourceCodeLine = 85;
            X = (ushort) ( Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) ) ; 
            __context__.SourceCodeLine = 87;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (X == 10))  ) ) 
                {
                __context__.SourceCodeLine = 88;
                X = (ushort) ( 0 ) ; 
                }
            
            __context__.SourceCodeLine = 90;
            PIN  .UpdateValue ( PIN + Functions.ItoA (  (int) ( X ) )  ) ; 
            __context__.SourceCodeLine = 92;
            PINLENGTH = (ushort) ( (PINLENGTH + 1) ) ; 
            __context__.SourceCodeLine = 94;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( PINLENGTH > 3 ))  ) ) 
                { 
                __context__.SourceCodeLine = 96;
                EVALUATEPINENTERED (  __context__ , PIN) ; 
                __context__.SourceCodeLine = 97;
                RESETVALUES (  __context__  ) ; 
                } 
            
            __context__.SourceCodeLine = 100;
            UPDATESTARS (  __context__  ) ; 
            __context__.SourceCodeLine = 101;
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
        
        __context__.SourceCodeLine = 106;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 0))  ) ) 
            { 
            } 
        
        __context__.SourceCodeLine = 108;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 110;
            RESETVALUES (  __context__  ) ; 
            } 
        
        __context__.SourceCodeLine = 113;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 2))  ) ) 
            { 
            __context__.SourceCodeLine = 115;
            PIN  .UpdateValue ( Functions.Left ( PIN ,  (int) ( 1 ) )  ) ; 
            __context__.SourceCodeLine = 116;
            PINLENGTH = (ushort) ( 1 ) ; 
            } 
        
        __context__.SourceCodeLine = 119;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (PINLENGTH == 3))  ) ) 
            { 
            __context__.SourceCodeLine = 121;
            PIN  .UpdateValue ( Functions.Left ( PIN ,  (int) ( 2 ) )  ) ; 
            __context__.SourceCodeLine = 122;
            PINLENGTH = (ushort) ( 2 ) ; 
            } 
        
        __context__.SourceCodeLine = 125;
        UPDATESTARS (  __context__  ) ; 
        __context__.SourceCodeLine = 126;
        CHECKBACKSPACEVIS (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}


public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
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
    
    PINSTARS = new Crestron.Logos.SplusObjects.StringOutput( PINSTARS__AnalogSerialOutput__, this );
    m_StringOutputList.Add( PINSTARS__AnalogSerialOutput__, PINSTARS );
    
    CURRENTUSERPIN = new Crestron.Logos.SplusObjects.StringOutput( CURRENTUSERPIN__AnalogSerialOutput__, this );
    m_StringOutputList.Add( CURRENTUSERPIN__AnalogSerialOutput__, CURRENTUSERPIN );
    
    MASTER_PIN = new StringParameter( MASTER_PIN__Parameter__, this );
    m_ParameterList.Add( MASTER_PIN__Parameter__, MASTER_PIN );
    
    USERPINFILELOCATION = new StringParameter( USERPINFILELOCATION__Parameter__, this );
    m_ParameterList.Add( USERPINFILELOCATION__Parameter__, USERPINFILELOCATION );
    
    
    INITIALIZE.OnDigitalPush.Add( new InputChangeHandlerWrapper( INITIALIZE_OnPush_0, false ) );
    for( uint i = 0; i < 10; i++ )
        NUM[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( NUM_OnPush_1, false ) );
        
    BACKSPACE.OnDigitalPush.Add( new InputChangeHandlerWrapper( BACKSPACE_OnPush_2, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_PIN_HANDLER ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint MASTER_PIN__Parameter__ = 10;
const uint USERPINFILELOCATION__Parameter__ = 11;
const uint INITIALIZE__DigitalInput__ = 0;
const uint BACKSPACE__DigitalInput__ = 1;
const uint NUM__DigitalInput__ = 2;
const uint BACKSPACEVIS__DigitalOutput__ = 0;
const uint CORRECTPIN__DigitalOutput__ = 1;
const uint WRONGPIN__DigitalOutput__ = 2;
const uint PINSTARS__AnalogSerialOutput__ = 0;
const uint CURRENTUSERPIN__AnalogSerialOutput__ = 1;

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
