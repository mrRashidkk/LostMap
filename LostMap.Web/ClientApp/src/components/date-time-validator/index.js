import React from "react";
import DateTimePicker from "../date-time-picker";
import { ValidatorComponent } from 'react-material-ui-form-validator';
import './style.css';

 class DateTimeValidator extends ValidatorComponent {
    renderValidatorComponent() {
      return (
        <div className='date-time-validator'>
          <DateTimePicker {...this.props} />
          {this.errorText()}
        </div>
      );
    }

    errorText() {
      return this.state.isValid 
        ? null 
        : <p className='error-text'>
            {this.getErrorMessage()}
          </p>;      
    }
}

export default DateTimeValidator;