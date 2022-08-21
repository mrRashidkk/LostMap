import React from "react";

const MapTooltip = ({ text }) => { 
  
  return (
    <div
      style={{
        background: 'white'
      }}
    >
      {text}
    </div>
  );
};

export default MapTooltip;