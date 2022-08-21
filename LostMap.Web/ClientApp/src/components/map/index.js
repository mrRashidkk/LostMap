import React, { useState, useEffect } from "react";
import { Map as PigeonMap, Marker, Overlay } from "pigeon-maps";
import api, { API_ROUTES } from "../../services/api";
import MapTooltip from "../map-tooltip";
import FindingDetailsDialog from "../finding-details-dialog";
import LossDetailsDialog from "../loss-details-dialog";
import NewReportDialog from "../new-report-dialog";

export default function Map() {
  const [center, setCenter] = useState([50.879, 4.6997]);
  const [zoom, setZoom] = useState(11);
  
  const [latitude, setLatitude] = useState(null);
  const [longitude, setLongitude] = useState(null);
  const [allFindings, setAllFindings] = useState([]);
  const [allLosses, setAllLosses] = useState([]);
  const [isTooltipVisible, setIsTooltipVisible] = useState(false);
  const [tooltipLocation, setTooltipLocation] = useState(null);
  const [tooltipText, setTooltipText] = useState('');

  const [selectedFinding, setSelectedFinding] = useState(null);
  const [selectedLoss, setSelectedLoss] = useState(null);

  const [openNewReportDialog, setOpenNewReportDialog] = useState(false);

  const loadAllFindings = async () => {
    const response = await api.get(API_ROUTES.getAllFindings);
    setAllFindings(response.data);
  };

  const loadAllLosses = async () => {
    const response = await api.get(API_ROUTES.getAllLosses);
    setAllLosses(response.data);
  }

  useEffect(() => {    
    loadAllFindings();
    loadAllLosses();
  }, []);

  const onSelectLocation = ({ event, latLng, pixel }) => {
    setLatitude(latLng[0]);
    setLongitude(latLng[1]);
  };

  const onMoverOverMarker = (anchor, { title }) => {
    setTooltipLocation(anchor);
    setTooltipText(title);
    setIsTooltipVisible(true);
  };

  const onMouseOutMarker = () => {
    setIsTooltipVisible(false);
    setTooltipText('');
    setTooltipLocation(null);
  }

  const onCreateLoss = () => {
    setOpenNewReportDialog(false);
    loadAllLosses();
  };

  const onCreateFinding = () => {
    setOpenNewReportDialog(false);
    loadAllFindings();
  };

  return (
    <>
      <PigeonMap 
        center={center} 
        zoom={zoom} 
        onBoundsChanged={({ center, zoom }) => { 
          setCenter(center) 
          setZoom(zoom) 
        }}
        onClick={onSelectLocation}
      >
        {latitude && longitude &&
          <Marker 
            width={50}
            anchor={[latitude, longitude]}
            onClick={() => setOpenNewReportDialog(true)}
          />
        }
        {allFindings.map((finding, index) => (
          <Marker
            key={index}
            width={50}
            color="green"
            anchor={[finding.latitude, finding.longitude]}
            onMouseOver={({anchor}) => onMoverOverMarker(anchor, finding)}
            onMouseOut={onMouseOutMarker}
            onClick={() => setSelectedFinding(finding)}
          />
        ))}
        {allLosses.map((loss, index) => (
          <Marker
            key={index}
            width={50}
            color="red"
            anchor={[loss.latitude, loss.longitude]}
            onMouseOver={({anchor}) => onMoverOverMarker(anchor, loss)}
            onMouseOut={onMouseOutMarker}
            onClick={() => setSelectedLoss(loss)}
          />
        ))}
        {isTooltipVisible && tooltipLocation && 
          <Overlay anchor={tooltipLocation} offset={[0,80]}>
            <MapTooltip text={tooltipText} />
          </Overlay>
        }        
      </PigeonMap>        
      {selectedFinding && 
        <FindingDetailsDialog 
          id={selectedFinding.id} 
          onClose={() => setSelectedFinding(null)}
        />
      }
      {selectedLoss &&
        <LossDetailsDialog
          id={selectedLoss.id}
          onClose={() => setSelectedLoss(null)}
        />
      }
      <NewReportDialog 
        open={openNewReportDialog} 
        latitude={latitude} 
        longitude={longitude}
        onClose={() => setOpenNewReportDialog(false)}
        onCreateLoss={onCreateLoss}
        onCreateFinding={onCreateFinding}
      />
    </>
  );
}