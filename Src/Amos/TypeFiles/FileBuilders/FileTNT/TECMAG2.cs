using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class TECMAG2
    {
        // Display Menu flags:
        public bool real_flag;				        // display real data				
        public bool imag_flag;				        // display imaginary data
        public bool magn_flag;				        // display magnitude data
        public bool axis_visible;			        // display axis
        public bool auto_scale;				        // auto scale mode on or off
        public bool line_display;			        // TRUE for lines, FALSE for points	
        public bool show_shim_units;		        // display shim units on the data area or not

        // Option Menu flags:
        public bool integral_display;				// integrals turned on? - but not swap area
        public bool fit_display;			        // fits turned on?  - but not swap area
        public bool show_pivot;			            // show pivot point on screen; only used during interactive phasing
        public bool label_peaks;			        // show labels on the peaks?
        public bool keep_manual_peaks;		        // keep manual peaks when re-applying peak pick settings?
        public bool label_peaks_in_units;		    // peak label type
        public bool integral_dc_average;			// use dc average for integral calculation
        public bool integral_show_multiplier;		// show multiplier on integrals that are scaled
        public bool[] Boolean_space;			

        // Processing flags:
        public bool[] all_ffts_done;
        public bool[] all_phase_done;			

        // Vertical display multipliers:
        public double amp;    					    // amplitude scale factor
        public double ampbits;					    // resolution of display
        public double ampCtl;			            // amplitude control value
        public int offset; 					        // vertical offset

        public GridAndAxis axis_set;			    // see Grid and Axis Structure below	

        public short[] display_units;               // display units for swap area
        public int[] ref_point;                     // for use in frequency offset calcs
        public double[] ref_value;                  // for use in frequency offset calcs
        public int z_start;		                    // beginning of data display (range: 0 to 2 * npts[0] - 2)
        public int z_end;					        // end of data display (range: 0 to 2 * npts[0] - 2)
        public int z_select_start;				    // beginning of zoom highlight
        public int z_select_end;				    // end of zoom highlight
        public int last_zoom_start;				    // last z_select_start - not used yet (4/10/97)
        public int last_zoom_end;				    // last z_select_end - not used yet (4/10/97)
        public int index_2D;				        // in 1D window, which 2D record we see
        public int index_3D;				        // in 1D window, which 3D record we see
        public int index_4D;				        // in 1D window, which 4D record we see				
	
        public int[] apodization_done;			    // masked value showing which processing has been done to the data; see constants.h for values
        public double[] linebrd;				    // line broadening value
        public double[] gaussbrd;				    // gaussian broadening value			
        public double[] dmbrd;				        // double exponential broadening value
        public double[] sine_bell_shift;			// sine bell shift value
        public double[] sine_bell_width;			// sine bell width value
        public double[] sine_bell_skew;			    // sine bell skew value
        public int[] Trapz_point_1;			        // first trapezoid point for trapezoidal apodization
        public int[] Trapz_point_2;			        // second trapezoid point for trapezoidal apodization	
        public int[] Trapz_point_3;			        // third trapezoid point for trapezoidal apodization	
        public int[] Trapz_point_4;			        // fourth trapezoid point for trapezoidal apodization
        public double[] trafbrd;				    // Traficante-Ziessow broadening value
        public int[] echo_center;				    // echo center for all dimensions

        public int data_shift_points;				// number of points to use in left/right shift operations
        public short[] fft_flag;				    // fourier transform done? false if time domain, true if frequency domain
        public double[] unused;				
        public int[] pivot_point;				    // for interactive phasing
        public double[] cumm_0_phase;			    // cummulative zero order phase applied
        public double[] cumm_1_phase;			    // cummulative first order phase applied
        public double manual_0_phase;				// used for interactive phasing
        public double manual_1_phase;				// used for interactive phasing
        public double phase_0_value;				// last zero order phase value applied (not necessarily equivalent to cummulative zero order phase)
        public double phase_1_value;				// last first order phase value applied (not necessarily equivalent to cummulative first order phase)
        public double session_phase_0;				// used during interactive phasing
        public double session_phase_1;				// used during interactive phasing

        public int max_index;				        // indexEvent of max data value
        public int min_index;				        // indexEvent of min data value
        public float peak_threshold;				// threshold above which peaks are chosen
        public float peak_noise;				    // minimum value between two points that are above the peak threshold to distinguish two peaks from two points on the same peak
        public short integral_dc_points;			// number of points to use in integral calculation when dc average is used
        public short integral_label_type;			// how to label integrals, see constants.h
        public float integral_scale_factor;			// scale factor to be used in integral draw
        public int auto_integrate_shoulder;			// number of points to determine where integral is cut off
        public double auto_integrate_noise;			// when average of shoulder points is under this value, cut off integral
        public double auto_integrate_threshold;	    // threshold above which a peak is chosen in auto integrate
        public int s_n_peak;				        // peak to be used for signal to noise calculation
        public int s_n_noise_start;				    // start of noise region for signal to noise calculation
        public int s_n_noise_end;				    // end of noise region for signal to noise calculation
        public float s_n_calculated;				// calculated signal to noise value 

        public int[] Spline_point;				    // points to be used for spline baseline fix calculation
        public short Spline_point_avr;				// for baseline fix
        public int[] Poly_point;				    // points for polynomial baseline fix calculation
        public short Poly_point_avr;				// for baseline fix
        public short Poly_order;				    // what order polynomial to use 

        //Blank Space:
        public byte[] space;				
							
        //Text variables:
        public string line_simulation_name;
        public string integral_template_name;		
        public string baseline_template_name;		
        public string layout_name;	
        public string relax_information_name;
        public string username;
        public string user_string_1;
        public string user_string_2;
        public string user_string_3;		
        public string user_string_4;
        
        public TECMAG2(BinaryReader bReader)
        {
            // Display Menu flags:
            real_flag = bReader.ReadInt32() == 1;			
            imag_flag = bReader.ReadInt32() == 1;
            magn_flag = bReader.ReadInt32() == 1;
            axis_visible = bReader.ReadInt32() == 1;
            auto_scale = bReader.ReadInt32() == 1;
            line_display = bReader.ReadInt32() == 1;	
            show_shim_units = bReader.ReadInt32() == 1;

            // Option Menu flags:
            integral_display = bReader.ReadInt32() == 1;
            fit_display = bReader.ReadInt32() == 1;
            show_pivot = bReader.ReadInt32() == 1;
            label_peaks = bReader.ReadInt32() == 1;
            keep_manual_peaks = bReader.ReadInt32() == 1;
            label_peaks_in_units = bReader.ReadInt32() == 1;
            integral_dc_average = bReader.ReadInt32() == 1;
            integral_show_multiplier = bReader.ReadInt32() == 1;
            Boolean_space = bReader.ReadBools(9);			

            // Processing flags:
            all_ffts_done = bReader.ReadBools(4);
            all_phase_done = bReader.ReadBools(4);

             // Vertical display multipliers:
            amp = bReader.ReadDouble();
            ampbits = bReader.ReadDouble();
            ampCtl = bReader.ReadDouble();
            offset = bReader.ReadInt32();

            axis_set = new GridAndAxis(bReader);

            display_units = bReader.ReadShorts(4);
            ref_point = bReader.ReadInts(4);
            ref_value = bReader.ReadDoubles(4);
            z_start = bReader.ReadInt32();
            z_end = bReader.ReadInt32();
            z_select_start = bReader.ReadInt32();
            z_select_end = bReader.ReadInt32();
            last_zoom_start = bReader.ReadInt32();
            last_zoom_end = bReader.ReadInt32();
            index_2D = bReader.ReadInt32();
            index_3D = bReader.ReadInt32();
            index_4D = bReader.ReadInt32();
	
            apodization_done = bReader.ReadInts(4);
            linebrd = bReader.ReadDoubles(4);
            gaussbrd = bReader.ReadDoubles(4);
            dmbrd = bReader.ReadDoubles(4);
            sine_bell_shift = bReader.ReadDoubles(4);
            sine_bell_width = bReader.ReadDoubles(4);
            sine_bell_skew = bReader.ReadDoubles(4);
            Trapz_point_1 = bReader.ReadInts(4);
            Trapz_point_2 = bReader.ReadInts(4);
            Trapz_point_3 = bReader.ReadInts(4);
            Trapz_point_4 = bReader.ReadInts(4);
            trafbrd = bReader.ReadDoubles(4);
            echo_center = bReader.ReadInts(4);

            data_shift_points = bReader.ReadInt32();
            fft_flag = bReader.ReadShorts(4);
            unused = bReader.ReadDoubles(8);
            pivot_point = bReader.ReadInts(4);
            cumm_0_phase = bReader.ReadDoubles(4);
            cumm_1_phase = bReader.ReadDoubles(4);
            manual_0_phase = bReader.ReadDouble();
            manual_1_phase = bReader.ReadDouble();
            phase_0_value = bReader.ReadDouble();
            phase_1_value = bReader.ReadDouble();
            session_phase_0 = bReader.ReadDouble();
            session_phase_1 = bReader.ReadDouble();

            max_index = bReader.ReadInt32();
            min_index = bReader.ReadInt32();
            peak_threshold = bReader.ReadSingle();
            peak_noise = bReader.ReadSingle();
            integral_dc_points = bReader.ReadInt16();
            integral_label_type = bReader.ReadInt16();
            integral_scale_factor = bReader.ReadSingle();
            auto_integrate_shoulder = bReader.ReadInt32();
            auto_integrate_noise = bReader.ReadDouble();
            auto_integrate_threshold = bReader.ReadDouble();
            s_n_peak = bReader.ReadInt32();
            s_n_noise_start = bReader.ReadInt32();
            s_n_noise_end = bReader.ReadInt32();
            s_n_calculated = bReader.ReadSingle();

            Spline_point = bReader.ReadInts(14);
            Spline_point_avr = bReader.ReadInt16();
            Poly_point = bReader.ReadInts(8);
            Poly_point_avr = bReader.ReadInt16();
            Poly_order = bReader.ReadInt16();

            space = bReader.ReadBytes(610);				
							
            line_simulation_name = Encoding.ASCII.GetString(bReader.ReadBytes(32));
            integral_template_name = Encoding.ASCII.GetString(bReader.ReadBytes(32));		
            baseline_template_name = Encoding.ASCII.GetString(bReader.ReadBytes(32));		
            layout_name = Encoding.ASCII.GetString(bReader.ReadBytes(32));	
            relax_information_name = Encoding.ASCII.GetString(bReader.ReadBytes(32));
            username = Encoding.ASCII.GetString(bReader.ReadBytes(32));
            user_string_1 = Encoding.ASCII.GetString(bReader.ReadBytes(16));
            user_string_2 = Encoding.ASCII.GetString(bReader.ReadBytes(16));
            user_string_3 = Encoding.ASCII.GetString(bReader.ReadBytes(16));		
            user_string_4 = Encoding.ASCII.GetString(bReader.ReadBytes(16));
        }
    }
}
