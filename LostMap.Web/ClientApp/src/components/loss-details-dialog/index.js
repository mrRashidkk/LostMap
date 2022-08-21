import React, { useState, useEffect } from "react";
import { Dialog } from "@mui/material";
import api, { API_ROUTES } from "../../services/api";

const LossDetailsDialog = ({ id, onClose }) => {
  const [loss, setLoss] = useState(null);

  useEffect(() => {
    const loadLoss = async () => {
      const url = `${API_ROUTES.getLoss}?id=${id}`;
      const response = await api.get(url);
      setLoss(response.data);
    };

    loadLoss();
  }, [id]);
  
  return (
    <Dialog open={true} onClose={onClose}>
      {!loss ? <div>Loading</div> :
        <>
          <div>{`Title: ${loss.title}`}</div>
          <div>{`Description: ${loss.description}`}</div>
          <div>{`Lost: ${loss.lost}`}</div>
        </>        
      }
    </Dialog>
  );
};

export default LossDetailsDialog;