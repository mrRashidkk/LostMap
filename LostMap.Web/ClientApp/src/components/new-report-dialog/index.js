import React, { useState, useEffect } from "react";
import { 
  Dialog,
  DialogTitle,  
  Radio, 
  RadioGroup,
  IconButton,
  Button, 
  FormControl, 
  FormControlLabel } from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';
import DateTimeValidator from "../date-time-validator";
import api, { API_ROUTES } from "../../services/api";
import { ValidatorForm, TextValidator } from 'react-material-ui-form-validator';
import { FORM_RULES, FORM_ERROR_MESSAGES } from "../../constants";
import './style.css';

const isValidDate = (val) => val instanceof Date && !isNaN(val);

const ENTITY_TYPE = {
  Finding: 'Finding',
  Loss: 'Loss'
};

const NewReportDialog = ({ open, latitude, longitude, onClose, onCreateLoss, onCreateFinding }) => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [entityType, setEntityType] = useState(ENTITY_TYPE.Finding);
  const [date, setDate] = useState(new Date());

  useEffect(() => {
    if (!ValidatorForm.hasValidationRule(FORM_RULES.IsValidDate)) {
      ValidatorForm.addValidationRule(FORM_RULES.IsValidDate, isValidDate);
    }    
    return () => {
      ValidatorForm.removeValidationRule(FORM_RULES.IsValidDate);
    };
  }, []);  

  const resetForm = () => {
    setTitle('');
    setDescription('')
    setDate(new Date());
  };

  const submit = async (event) => {
    if (event) event.preventDefault();

    const dateName = entityType === ENTITY_TYPE.Finding ? 'found' : 'lost';
    const data = {
      title,
      description,
      latitude,
      longitude,
      [dateName]: date
    };
    
    if (entityType === ENTITY_TYPE.Finding) {
      await api.post(API_ROUTES.saveFinding, data);
      onCreateFinding();
    } else {
      await api.post(API_ROUTES.saveLoss, data);
      onCreateLoss();
    }
    resetForm();    
  };  

  const handleClose = () => {
    resetForm();
    onClose();
  };

  return (
    <Dialog 
      open={open} 
      className='new-report-dialog'
      maxWidth='sm'
      fullWidth
    >
      <DialogTitle>
        <span>Report a finding or loss</span>
        <IconButton onClick={handleClose} className='close-button'>
          <CloseIcon />
        </IconButton>
      </DialogTitle>      
        <ValidatorForm onSubmit={submit} className='form'>
          <FormControl fullWidth className='form-control'>
            <RadioGroup
              row
              value={entityType}
              onChange={e => setEntityType(e.target.value)}
            >
              <FormControlLabel value={ENTITY_TYPE.Finding} control={<Radio />} label="Finding" />
              <FormControlLabel value={ENTITY_TYPE.Loss} control={<Radio />} label="Loss" />
            </RadioGroup>
          </FormControl>
          <TextValidator
            label="Title"
            variant="outlined"
            fullWidth
            value={title}
            onChange={e => setTitle(e.target.value)}
            validators={[FORM_RULES.Required]}
            errorMessages={[FORM_ERROR_MESSAGES.Required]}
            className='form-control'
          />
          <TextValidator
            multiline
            rows={3}
            label="Description"
            variant="outlined"
            fullWidth
            value={description}
            onChange={e => setDescription(e.target.value)}
            validators={[FORM_RULES.Required]}
            errorMessages={[FORM_ERROR_MESSAGES.Required]}
            className='form-control'
          />
          <div className='form-control'>
            <DateTimeValidator
              label="When found/lost"
              value={date}
              onChange={(newVal) => setDate(newVal)}
              validators={[FORM_RULES.IsValidDate]}
              errorMessages={[FORM_ERROR_MESSAGES.InvalidDate]}
            />
          </div>
          <div className='actions'>
            <Button 
              variant="outlined"
              onClick={handleClose}
              className='button'
            >Cancel</Button>
            <Button 
              type='submit'
              variant="contained"
              className='button'
            >Submit</Button>
          </div>                  
        </ValidatorForm>
    </Dialog>
  );
}

export default NewReportDialog;