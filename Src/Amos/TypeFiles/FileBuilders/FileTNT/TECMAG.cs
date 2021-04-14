using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class TECMAG
    {
        // Number of points and scans in all dimensions:
        //public int[] npts;                          // points requested 1D, 2D, 3D, 4D
        //public int[] actual_npts;                   // points completed in each dimension (actual_npts[0] is not really used) 
        //public int acq_points;                      // acq_points will be number of points to acquire during one acquisition icon in the sequence (which may be smaller than npts[0])
        //public int[] npts_start;                    // scan or pt on which to start the acquisition
        //public int scans;					        // scans 1D requested
        //public int actual_scans;				    // scans 1D completed
        //public int dummy_scans;                     // number of scans to do prior to collecting actual data  
        //public int repeat_times;                    // Number of times to repeat scan  
        //public int sadimension;                     // signal average dimension 
        //public int samode;                          // sets behavior of the signal averager for the dimension specified in S.A. Dimension

        // Field and frequencies:
        //public double magnet_field;                 // magnet field
        //public double[] ob_freq;                    // observe frequency
        //public double[] base_freq;                  // base frequency
        //public double[] offset_freq;                // offset from base
        //public double ref_freq;                     // reference frequency for axis calculation (used to be freqOffset)
        //public double NMR_frequency;				// absolute Amos frequency
        //public short obs_channel;				    // observe channel defalut = 1;
        //public byte[] space2;		                // 42 bytes

        // Spectral width, dwell and filter:
        //public double[] sw;                         // spectral width in Hz
        //public double[] dwell;                      // dwell time in seconds
        //public double filter;                       // filter	
        public double experiment_time;              // time for whole experiment
        //public double acq_time;                     // acquisition time - time for acquisition
        //public double last_delay;                   // last delay in seconds
        public short spectrum_direction;            // 1 or -1
        public short hardware_sideband;
        public short Taps;                          // number of taps on receiver filter
        public short Type;					        // type of filter
        public bool bDigRec;		                // toggle for digital receiver
        public int nDigitalCenter;		            // number of shift points for digital receiver
        //public byte[] space3;                       // 16 bytes

        // Hardware settings:
        //public short transmitter_gain;			    // transmitter gain
        //public short receiver_gain;		            // receiver gain
        public short NumberOfReceivers;  	        // number of Rx in MultiRx system
        public short RG2;				            // receiver gain for Rx channel 2	
        //public double receiver_phase;		        // receiver phase
        //public byte[] space4;                       // 4 bytes

        // Spinning speed information:
        //public ushort set_spin_rate;                // set spin rate
        //public ushort actual_spin_rate;		        // actual spin rate read from the meter
		
        // Lock information:
        //public short lock_field;		            // lock field value (might be Bruker specific)
        //public short lock_power;	                // lock transmitter power
        //public short lock_gain;			            // lock receiver gain
        //public short lock_phase;		            // lock phase	
        public double lock_freq_mhz;	            // lock frequency in MHz
        //public double lock_ppm;		                // lock ppm
        public double H2O_freq_ref;		            // H1 freq of H2O
        //public byte[] space5;		                // 16 bytes

        // VT information:
        //public double set_temperature;		        // non-integer VT
        //public double actual_temperature;	        // non-integer VT

        // Shim information:
        //public double shim_units;			        // shim units (used to be SU)	
        public short[] shims;				        // shim values
        public double shim_FWHM;				    // full width at half maximum
 	
        // Bruker specific information:
        //public short HH_dcpl_attn;				    // decoupler attenuation (0..63 or 100..163); receiver gain is above
        public short DF_DN;				            // decoupler
        public short[] F1_tran_mode;		        // F1 Pulse transmitter switches
        //public short dec_BW;		                // decoupler BW
        //public byte[] grd_orientation;              // gradient orientation
        public int LatchLP;			                // 990629JMB  values for lacthed LP board
        //public double grd_Theta;          		    // 990720JMB  gradient rotation angle Theta
        //public double grd_Phi;			            // 990720JMB  gradient rotation angle Phi
        //public byte[] space6;                       // 264 bytes

        // Time variables 
        //public DateTime start_time;		            // 4 bytes starting time
        //public DateTime finish_time;	            // 4 bytes finishing time
        //public TimeSpan elapsed_time;		        // 4 bytes projected elapsed time text below and variables above

        // Text variables: 96 below
        //public byte[] date;	                        // experiment date
        //public byte[] nucleus;	                    // nucleus
        public byte[] nucleus_2D;                   // 2D nucleus
        public byte[] nucleus_3D;                   // 3D nucleus
        public byte[] nucleus_4D;                   // 4D nucleus
        public byte[] sequence;                     // sequence name
        //public byte[] lock_solvent;                 // Lock solvent
        public byte[] lock_nucleus;                 // Lock nucleus

        public TECMAG( BinaryReader bRiader, Document document )
        {
            // Number of points and scans in all dimensions:
            for ( int i = 1; i <= 4; i++ )
                document.Dashboard.SetVariable( string.Format( "Points {0}D", i ), bRiader.ReadInt32() );      // npts
            document.Dashboard.SetVariable( "Points 1D", bRiader.ReadInt32() );
            for ( int i = 2; i <= 4; i++ )
                document.Dashboard.SetVariable( string.Format( "Actual Points {0}D", i ), bRiader.ReadInt32() );       //actual_npts
            document.Dashboard.SetVariable( "Acq. Points", bRiader.ReadInt32() );      // acq_points
            document.Dashboard.SetVariable( "Scan Start 1D", bRiader.ReadInt32() );    // npts_start[0]
            for ( int i = 2; i <= 4; i++ )
                document.Dashboard.SetVariable( string.Format( "Points Start {0}D", i ), bRiader.ReadInt32() );    // npts_start[1-3] 
            document.Dashboard.SetVariable( "Scans 1D", bRiader.ReadInt32());          //scans
            document.Dashboard.SetVariable( "Actual Scans 1D", bRiader.ReadInt32() );  //actual_scans
            document.Dashboard.SetVariable( "Dummy Scans", bRiader.ReadInt32() );      //dummy_scans
            document.Dashboard.SetVariable( "Repeat Times", bRiader.ReadInt32() );     //repeat_times;

            bRiader.ReadInt32();        // sadimension = Нет необходимости загружать
            bRiader.ReadInt32();        // samode =  Нет необходимости загружать

            // Field and frequencies:
            document.Dashboard.SetVariable( "Magnet Field", bRiader.ReadDouble() );        //magnet_field
            for ( int i = 1; i <= 4; i++ )
                 document.Dashboard.SetVariable( string.Format( "F{0} Freq.", i ), bRiader.ReadDouble() * 1000000 );   //ob_freq
            for ( int i = 1; i <= 4; i++ )
                document.Dashboard.SetVariable( string.Format( "F{0} Base Freq.", i ), bRiader.ReadDouble() * 1000000 );    //base_freq
            for ( int i = 1; i <= 4; i++ )
                document.Dashboard.SetVariable( string.Format( "F{0} Offset Freq.", i ), bRiader.ReadDouble() * 1000);    //offset_freq
            document.Dashboard.SetVariable( "Obs. Ref. Freq.", bRiader.ReadDouble() * 1000000  );      //ref_freq
            document.Dashboard.SetVariable( "Absolute Freq.",  bRiader.ReadDouble() * 1000000  );      //NMR_frequency
            document.Dashboard.SetVariable( "Observe Ch.", bRiader.ReadInt16() );          //obs_channel;
            bRiader.ReadBytes(42);

            // Spectral width, dwell and filter:
            document.Dashboard.SetVariable( "SW +/-", bRiader.ReadDouble() );      // sw[0]
            for ( int i = 2; i <= 4; i++ )
                document.Dashboard.SetVariable( string.Format( "SW {0}D", i ), bRiader.ReadDouble() );     // sw[1-3]
            document.Dashboard.SetVariable( "Dwell Time", bRiader.ReadDouble() );      // dwell[0]
            for ( int i = 2; i <= 4; i++ )
                document.Dashboard.SetVariable( string.Format( "Dwell {0}D", i ), bRiader.ReadDouble() );     // dwell[1-3]
            document.Dashboard.SetVariable( "Filter", bRiader.ReadDouble() );     // filter

            experiment_time = bRiader.ReadDouble();

            document.Dashboard.SetVariable( "Acq. Time", bRiader.ReadDouble() );       // acq_time;
            document.Dashboard.SetVariable( "Last Delay", bRiader.ReadDouble() );      // last_delay

            spectrum_direction = bRiader.ReadInt16();
            hardware_sideband = bRiader.ReadInt16();
            Taps = bRiader.ReadInt16();
            Type = bRiader.ReadInt16();
            bDigRec = bRiader.ReadInt32() == 1;
            nDigitalCenter = bRiader.ReadInt32();
            bRiader.ReadBytes(16);

            // Hardware settings:
            document.Dashboard.SetVariable( "Trans. Gain", bRiader.ReadInt16() );        // transmitter_gain
            document.Dashboard.SetVariable( "Receiver Gain", bRiader.ReadInt16() );        // receiver_gain

            NumberOfReceivers = bRiader.ReadInt16();
            RG2 = bRiader.ReadInt16();

            document.Dashboard.SetVariable( "Receiver Phase", bRiader.ReadDouble() );       //receiver_phase
            bRiader.ReadBytes(4);

            // Spinning speed information:
            bRiader.ReadUInt16();               // set_spin_rate = Нет необходимости загружать
            bRiader.ReadUInt16();               // actual_spin_rate = Нет необходимости загружать
		
            // Lock information:
            document.Dashboard.SetVariable( "Lock Field", bRiader.ReadInt16());        // lock_field = ;
            document.Dashboard.SetVariable( "Lock Power",bRiader.ReadInt16());         // lock_power = ;
            document.Dashboard.SetVariable( "Lock Gain",bRiader.ReadInt16());          // lock_gain = ;
            document.Dashboard.SetVariable( "Lock Phase", bRiader.ReadInt16());        // lock_phase =;

            lock_freq_mhz = bRiader.ReadDouble();

            document.Dashboard.SetVariable( "Lock ppm",bRiader.ReadDouble());          // lock_ppm = ;

            H2O_freq_ref = bRiader.ReadDouble();
            bRiader.ReadBytes(16);		

            // VT information:
            document.Dashboard.SetVariable("Set Temp.", bRiader.ReadDouble());         // set_temperature = ;
            document.Dashboard.SetVariable("Actual Temp.",bRiader.ReadDouble());;      // actual_temperature = ;

            // Shim information:
            document.Dashboard.SetVariable("Shim Units", bRiader.ReadDouble());        // shim_units =;
            shims = bRiader.ReadShorts(36);
            shim_FWHM = bRiader.ReadDouble();
 	
            // Bruker specific information:
            document.Dashboard.SetVariable("Dec. Attn.",bRiader.ReadInt16());      //HH_dcpl_attn = ;

            DF_DN = bRiader.ReadInt16();
            F1_tran_mode= bRiader.ReadShorts(7);

            document.Dashboard.SetVariable("Dec. BW", bRiader.ReadInt16());        // dec_BW = ;
            document.Dashboard.SetVariable("Grd. Orientation", Encoding.ASCII.GetString( bRiader.ReadBytes( 4 ) ) );       // grd_orientation =;

            LatchLP = bRiader.ReadInt32();

            document.Dashboard.SetVariable( "Grd. Theta", bRiader.ReadDouble());       // grd_Theta = ;
            document.Dashboard.SetVariable("Grd. Phi", bRiader.ReadDouble());           // grd_Phi = ;
            bRiader.ReadBytes(264);

            // Time variables 
            document.Dashboard.SetVariable( "Exp. Start Time", bRiader.ReadCTime() );        //start_time
            document.Dashboard.SetVariable( "Exp. Finish Time", bRiader.ReadCTime() );       //finish_time
            document.Dashboard.SetVariable( "Exp. Elapsed Time", bRiader.ReadCTimeSpan() );  //elapsed_time

            // Text variables: 96 below
            document.Dashboard.SetVariable( "Date", Encoding.ASCII.GetString( bRiader.ReadBytes( 32 ) ) );     //date
            document.Dashboard.SetVariable( "Nucleus", Encoding.ASCII.GetString( bRiader.ReadBytes( 16 ) ) );   //nucleus
            nucleus_2D = bRiader.ReadBytes( 16 );
            nucleus_3D = bRiader.ReadBytes( 16 );
            nucleus_4D = bRiader.ReadBytes( 16 );
            sequence = bRiader.ReadBytes( 32 );
            document.Dashboard.SetVariable( "Lock Solvent", Encoding.ASCII.GetString( bRiader.ReadBytes( 16 ) ) );     // lock_solvent;
            lock_nucleus = bRiader.ReadBytes( 16 );
        }
    }
}
