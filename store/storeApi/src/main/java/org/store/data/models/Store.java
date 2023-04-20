package org.store.data.models;

import java.util.UUID;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class Store {
	
	private Integer id;	
	private UUID storeCode;
	private String address;
	
}
